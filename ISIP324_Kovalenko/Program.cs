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
            List<Student> students = new List<Student>();
            List<Course> courses = new List<Course>();
            List<Teacher> teachers = new List<Teacher>();

            void AddStudent()
            {
                Console.WriteLine("Введите имя студента:");
                string name = Console.ReadLine();
                Console.WriteLine("Введите фамилию студента:");
                string surname = Console.ReadLine();
                Console.WriteLine("Введите возраст студента:");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите контактную информацию студента:");
                string contactInfo = Console.ReadLine();
                Console.WriteLine("Введите номер зачетной книжки студента:");
                int recordBookNumber = int.Parse(Console.ReadLine());
                Student newStudent = new Student(name, surname, age, contactInfo, recordBookNumber);
                students.Add(newStudent);
                Console.WriteLine("Студент успешно добавлен.");
            }

            void ViewStudentInfo()
            {
                Console.WriteLine("Введите ID студента для просмотра информации:");
                int id = int.Parse(Console.ReadLine());
                Student student = students.FirstOrDefault(s => s.GetId() == id);
                if (student != null)
                {
                    student.DisplayInfo();
                }
                else
                {
                    Console.WriteLine("Студент с таким ID не найден.");
                }
            }

            void EnrollStudentInCourse()
            {
                Console.WriteLine("Введите ID студента для записи на курс:");
                int studentId = int.Parse(Console.ReadLine());
                Student student = students.FirstOrDefault(s => s.GetId() == studentId);
                if (student != null)
                {
                    Console.WriteLine("Введите ID курса для записи студента:");
                    int courseId = int.Parse(Console.ReadLine());
                    Course course = courses.FirstOrDefault(c => c.courseId == courseId);
                    if (course != null)
                    {
                        student.EnrollInCourse(course);
                        Console.WriteLine("Студент успешно записан на курс.");
                    }
                    else
                    {
                        Console.WriteLine("Курс с таким ID не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Студент с таким ID не найден.");
                }
            }
            void ViewStudentCourses()
            {
                Console.WriteLine("Введите ID студента для просмотра списка курсов:");
                int studentId = int.Parse(Console.ReadLine());
                Student student = students.FirstOrDefault(s => s.GetId() == studentId);
                if (student != null)
                {
                    Console.WriteLine($"Список курсов студента {student.name} {student.surname}:");
                    foreach (var course in student.enrolledCourses)
                    {
                        Console.WriteLine($"- {course.courseName}");
                    }
                }
                else
                {
                    Console.WriteLine("Студент с таким ID не найден.");
                }
            }

            void AddTeacher()
            {
                Console.WriteLine("Введите имя преподавателя:");
                string name = Console.ReadLine();
                Console.WriteLine("Введите фамилию преподавателя:");
                string surname = Console.ReadLine();
                Console.WriteLine("Введите возраст преподавателя:");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите контактную информацию преподавателя:");
                string contactInfo = Console.ReadLine();
                Console.WriteLine("Введите кафедру преподавателя:");
                string department = Console.ReadLine();
                Console.WriteLine("Введите квалификацию преподавателя:");
                string qualification = Console.ReadLine();
                Teacher newTeacher = new Teacher(name, surname, age, contactInfo, department, qualification);
                teachers.Add(newTeacher);
                Console.WriteLine("Преподаватель успешно добавлен.");
            }

            void ViewTeacherInfo()
            {
                Console.WriteLine("Введите ID преподавателя для просмотра информации:");
                int id = int.Parse(Console.ReadLine());
                Teacher teacher = teachers.FirstOrDefault(t => t.GetId() == id);
                if (teacher != null)
                {
                    teacher.DisplayInfo();
                }
                else
                {
                    Console.WriteLine("Преподаватель с таким ID не найден.");
                }
            }

            void AssignTeacherToCourse()
            {
                Console.WriteLine("Введите ID преподавателя для назначения на курс:");
                int teacherId = int.Parse(Console.ReadLine());
                Teacher teacher = teachers.FirstOrDefault(t => t.GetId() == teacherId);
                if (teacher != null)
                {
                    Console.WriteLine("Введите ID курса для назначения преподавателя:");
                    int courseId = int.Parse(Console.ReadLine());
                    Course course = courses.FirstOrDefault(c => c.courseId == courseId);
                    if (course != null)
                    {
                        course.teacher = teacher;
                        teacher.AssignCourse(course);
                        Console.WriteLine("Преподаватель успешно назначен на курс.");
                    }
                    else
                    {
                        Console.WriteLine("Курс с таким ID не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Преподаватель с таким ID не найден.");
                }
            }
            void AddCourse()
            {
                Console.WriteLine("Введите название курса:");
                string courseName = Console.ReadLine();
                Console.WriteLine("Введите ID преподавателя для назначения на курс:");
                int teacherId = int.Parse(Console.ReadLine());
                Teacher teacher = teachers.FirstOrDefault(t => t.GetId() == teacherId);
                if (teacher != null)
                {
                    Course newCourse = new Course(courseName, teacher);
                    courses.Add(newCourse);
                    teacher.AssignCourse(newCourse);
                    Console.WriteLine("Курс успешно создан.");
                }
                else
                {
                    Console.WriteLine("Преподаватель с таким ID не найден.");
                }
            }
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


            Console.WriteLine("Управление университетом:");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Просмотреть информацию о студенте");
            Console.WriteLine("3. Записать студента на курс");
            Console.WriteLine("4. Просмотреть список курсов студента");
            Console.WriteLine("5. Добавить преподавателя");
            Console.WriteLine("6. Просмотреть информацию о преподавателе");
            Console.WriteLine("7. Назначить преподавателя на курс");
            Console.WriteLine("8. Создать новый курс");
            Console.WriteLine("9. Просмотреть информацию о курсе");
            Console.WriteLine("10. Вывести список студентов курса");
            Console.WriteLine("11. Вывести полный список студентов");
            Console.WriteLine("12. Вывести полный список преподавателей");
            Console.WriteLine("13. Вывести полный список курсов");
            Console.WriteLine("0. Выход");
        }
    }
}
