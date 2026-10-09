using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    internal class Program
    {
        class Person
        {
            public string name;
            public int age;
            private int id;
            public string contactInfo;
            private static int nextId = 0;
            public string surname;
            public Person(string name, string surname, int age, string contactInfo)
            {
                this.name = name;
                this.surname = surname;
                this.age = age;
                this.contactInfo = contactInfo;
                this.id = nextId++;
            }

            public void DisplayInfo()
            {
                Console.WriteLine($"ID: {id}, Имя: {name} {surname}, Возраст: {age}, Контактная информация: {contactInfo}");
            }

            public int GetId()
            {
                return id;
            }

        }
        class Course
        {
            public string courseName;
            public int courseId;
            private static int nextCourseId = 0;
            public Teacher teacher;
            public List<Student> enrolledStudents;
            public Course(string courseName, Teacher teacher)
            {
                this.courseName = courseName;
                this.courseId = nextCourseId++;
                this.teacher = teacher;
                this.enrolledStudents = new List<Student>();
            }

            public void AddStudent(Student student)
            {
                enrolledStudents.Add(student);
            }

            public void DisplayCourseInfo()
            {
                Console.WriteLine($"ID курса: {courseId}, Название курса: {courseName}, Преподаватель: {teacher.name} {teacher.surname}");
                Console.WriteLine("Записанные студенты:");
                foreach (var student in enrolledStudents)
                {
                    Console.WriteLine($"- {student.name} {student.surname}");
                }
            }

        }
        class Student : Person
        {
            public int recordBookNumber;
            public List<Course> enrolledCourses;

            public Student(string name, string surname, int age, string contactInfo, int recordBookNumber) : base(name, surname, age, contactInfo)
            {
                this.recordBookNumber = recordBookNumber;
                this.enrolledCourses = new List<Course>();
            }

            public void EnrollInCourse(Course course)
            {
                enrolledCourses.Add(course);
                course.AddStudent(this);
            }

            public override void DisplayInfo()
            {
                base.DisplayInfo();
                Console.WriteLine($"Номер зачетной книжки: {recordBookNumber}");
                Console.WriteLine("Записанные курсы:");
                foreach (var course in enrolledCourses)
                {
                    Console.WriteLine($"- {course.courseName}");
                }
            }
        }
        
    
        class Teacher : Person
        {
            public string department;
            public string qualification;
            public List<Course> coursesTaught;
            public Teacher(string name, string surname, int age, string contactInfo, string department, string qualification) : base(name, surname, age, contactInfo)
            {
                this.department = department;
                this.qualification = qualification;
                this.coursesTaught = new List<Course>();
            }

            public void AssignCourse(Course course)
            {
                coursesTaught.Add(course);
            }
            
            public override void DisplayInfo()
            {
                base.DisplayInfo();
                Console.WriteLine($"Кафедра: {department}");
                Console.WriteLine($"Квалификация: {qualification}");
                Console.WriteLine("Ведомые курсы:");
                foreach (var course in coursesTaught)
                {
                    Console.WriteLine($"- {course.courseName}");
                }
            }
        }
        static void Main(string[] args)
        {
//            Для выполнения задания используйте все возможности языка C#, изученные ранее (классы, списки, перечисления, LINQ и так далее).Обязательно сделайте проверку всевозможных вводимых значений (не должно быть возможности создать пустой товар, с отрицательной ценой, с отрицательным количеством и тому подобное).Программа не должна вылетать в процессе работы.Программа должна выводить информацию в чётком и ясном виде для пользователя.Не забудьте отправлять код по частям, разными коммитами, и делать осмысленные комментарии к коммитам.

//Вам необходимо создать систему управления университетом. Система должна позволять управлять информацией о студентах, преподавателях и курсах через консоль.

//В университете есть студенты, которые могут записываться на различные курсы.У каждого курса есть преподаватель, который его ведет. Система должна хранить информацию обо всех участниках учебного процесса и позволять выполнять различные операции с ними.

//Пользователь должен иметь возможность добавлять в систему новых студентов и просматривать информацию о них. Также необходимо реализовать функциональность записи студентов на курсы и просмотра списка всех курсов, на которые записан конкретный студент.

//Система должна позволять добавлять преподавателей и просматривать информацию о каждом из них. Преподаватели могут быть назначены на различные курсы, которые они будут вести.

//Для управления курсами нужно реализовать возможность создания новых курсов, просмотра детальной информации о каждом курсе и вывода списка всех студентов, записанных на конкретный курс.

//Дополнительно программа должна предоставлять возможность вывода полных списков: всех студентов в системе, всех преподавателей и всех доступных курсов.

//Ваша задача -спроектировать архитектуру приложения, используя принципы ООП, и реализовать консольное меню для удобного взаимодействия со всеми описанными функциями системы.

//Требования к проектированию

//При разработке системы вы обязаны применить принципы ООП:

//            1.Абстракция

//Выделите общие характеристики и поведение для похожих сущностей.

//2.Наследование

//Студенты и преподаватели имеют общие характеристики(имя, возраст, контактная информация и т.д.). Используйте наследование, чтобы избежать дублирования кода.

//3.Инкапсуляция

//Данные объектов должны быть защищены от прямого доступа. Подумайте, какие поля должны быть приватными, какие методы публичными.

//4.Полиморфизм

//Разные типы людей в университете могут иметь разное представление своей информации.Реализуйте возможность работы с объектами через базовый класс.



        }
    }
}
