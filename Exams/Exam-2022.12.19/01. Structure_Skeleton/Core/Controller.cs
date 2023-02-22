using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private IRepository<ISubject> subjects;
        private IRepository<IStudent> students;
        private IRepository<IUniversity> universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }
        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(TechnicalSubject) && subjectType != nameof(HumanitySubject) && subjectType != nameof(EconomicalSubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            if (subjects.FindByName(subjectName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            ISubject subject;
            var id = subjects.Models.Count + 1;

            if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(id, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(id, subjectName);
            }
            else
            {
                subject = new EconomicalSubject(id, subjectName);
            }

            subjects.AddModel(subject);

            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, "SubjectRepository");
        }


        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            var id = universities.Models.Count + 1;

            ICollection<int> collection = new List<int>();  // !!!

            foreach (var item in requiredSubjects)
            {
                ISubject subject = subjects.FindByName(item);
                collection.Add(subject.Id);
            }

            IUniversity university = new University(id, universityName, category, capacity, collection);

            universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, "UniversityRepository");
        }

        public string AddStudent(string firstName, string lastName)
        {
            var studentName = firstName + " " + lastName;

            if (students.FindByName(studentName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            var id = students.Models.Count + 1;
            IStudent student = new Student(id, firstName, lastName);
            students.AddModel(student);

            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, "StudentRepository");
        }

        public string TakeExam(int studentId, int subjectId)
        {
            if (students.FindById(studentId) == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }
            if (subjects.FindById(subjectId) == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }

            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);

            if (student.CoveredExams.ToList().Contains(subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);   // !!!
            }

            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }


        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] input = studentName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string firstName = input[0];
            string lastName = input[1];

            if (students.FindByName(studentName) == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
            }

            if (universities.FindByName(universityName) == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            IUniversity university = universities.FindByName(universityName);
            IStudent student = students.FindByName(studentName);

            List<int> universityObjects = university.RequiredSubjects.OrderBy(x => x).ToList();
            List<int> studentObjects = student.CoveredExams.OrderBy(x => x).ToList();

            bool isCovered = true;

            if (universityObjects.Count == studentObjects.Count)
            {
                for (int i = 0; i < universityObjects.Count; i++)
                {
                    if (universityObjects[i] != studentObjects[i])
                    {
                        isCovered = false;
                        break;
                    }
                }
            }
            else
            {
                isCovered = false;
            }


            if (!isCovered)    // !!!!!!!!!!!!
            {
                return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }

            if (student.University == university)
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
            }

            student.JoinUniversity(university);

            return string.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);
        }


        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            int studentsCount = students.Models.Where(x => x.University == university).Count();
            int capacityLeft = university.Capacity - studentsCount;

            if (capacityLeft < 0)
            {
                capacityLeft = 0;
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {capacityLeft}");

            return sb.ToString().TrimEnd();
        }
    }
}
