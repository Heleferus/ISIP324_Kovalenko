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
            private static int nextId = 1; 
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

            public virtual int GetId()
            {
                return id;
            }
        }

        class Course
        {
            public string courseName;
            public int courseId;
            private static int nextCourseId = 1;
            public Teacher teacher;
            public List<Student> enrolledStudents;

            public Course(string courseName, Teacher teacher)
            {
                this.courseName = courseName;
                this.courseId = nextCourseId++;
                this.teacher = teacher;
                this.enrolledStudents = new List<Student>();
            }

            public int GetId()
            {
                return courseId;
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


            public new void DisplayInfo() 
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



            public new void DisplayInfo()
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
                List<Student> students = new List<Student>
                {
                    new Student("Гриша", "Ковальчук", 20, "ger@mail.ru", 12345),
                    new Student("Саша", "Петров", 21, "sasha@mail.ru", 12346),
                    new Student("Маша", "Иванова", 22, "masha@mail.ru", 12347)
                };

                List<Teacher> teachers = new List<Teacher>
                {
                    new Teacher ("Иван", "Сидоров", 35, "ivan@mail.ru", "Кафедра математики", "Кандидат наук"),
                    new Teacher ("Петр", "Алексеев", 40, "petr@mail.ru", "Кафедра физики", "Доктор наук"),
                    new Teacher ("Мария", "Кузнецова", 30, "masha@mail.ru", "Кафедра химии", "Кандидат наук")
                };

                List<Course> courses = new List<Course>
                {
                    new Course("Математика", teachers[0]),
                    new Course("Физика", teachers[1]),
                    new Course("Химия", teachers[2])
                };
                

                void AddStudent()
                {
                    Console.WriteLine("Введите имя студента:");
                    string name = Console.ReadLine();
                    if (name == null || name.Trim() == "")
                    {
                    Console.WriteLine("Имя не может быть пустым. Попробуйте снова.");
                    }
                    Console.WriteLine("Введите фамилию студента:");
                    string surname = Console.ReadLine();
                    if (surname == null || surname.Trim() == "")
                    {
                    Console.WriteLine("Фамилия не может быть пустой. Попробуйте снова.");
                    }
                    Console.WriteLine("Введите возраст студента:");
                    int age = int.TryParse(Console.ReadLine(), out age) ? age : 0;
                    if (age < 0)
                    {
                    Console.WriteLine("Возраст не может быть отрицательным. Попробуйте снова.");
                    return;
                    }
                    Console.WriteLine("Введите контактную информацию студента:");
                    string contactInfo = Console.ReadLine();
                    if (contactInfo == null || contactInfo.Trim() == "")
                    {
                    Console.WriteLine("Контактная информация не может быть пустой. Попробуйте снова.");
                    return;
                    }
                    Console.WriteLine("Введите номер зачетной книжки студента:");
                    int recordBookNumber = int.TryParse(Console.ReadLine(), out recordBookNumber) ? recordBookNumber : 0;
                    Student newStudent = new Student(name, surname, age, contactInfo, recordBookNumber);
                    students.Add(newStudent);
                    Console.WriteLine($"Студент успешно добавлен. Его ID: {newStudent.GetId()}");
                }

                void ViewStudentInfo()
                {
                    Console.WriteLine("Введите ID студента для просмотра информации:");
                    int id = int.TryParse(Console.ReadLine(), out id) ? id : 0;
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
                    int studentId = int.TryParse(Console.ReadLine(), out studentId) ? studentId : 0;
                    Student student = students.FirstOrDefault(s => s.GetId() == studentId);
                    if (student != null)
                    {
                        Console.WriteLine("Введите ID курса для записи студента:");
                        int courseId = int.TryParse(Console.ReadLine(), out courseId) ? courseId : 0;
                        Course course = courses.FirstOrDefault(c => c.GetId() == courseId); 
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
                    int studentId = int.TryParse(Console.ReadLine(), out studentId) ? studentId : 0;
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
                    if (name == null || name.Trim() == "")
                    {
                    Console.WriteLine("Имя не может быть пустым. Попробуйте снова.");
                    return;
                    }
                    Console.WriteLine("Введите фамилию преподавателя:");
                    string surname = Console.ReadLine();
                    Console.WriteLine("Введите возраст преподавателя:");
                    int age = int.TryParse(Console.ReadLine(), out age) ? age : 0;
                    Console.WriteLine("Введите контактную информацию преподавателя:");
                    string contactInfo = Console.ReadLine();
                    if (contactInfo == null || contactInfo.Trim() == "")
                    {
                        Console.WriteLine("Контактная информация не может быть пустой. Попробуйте снова.");
                        return;
                    }
                    Console.WriteLine("Введите кафедру преподавателя:");
                    string department = Console.ReadLine();
                    if (department == null || department.Trim() == "")
                    {
                    Console.WriteLine("Кафедра не может быть пустой. Попробуйте снова.");
                    return;
                    }
                    Console.WriteLine("Введите квалификацию преподавателя:");
                    string qualification = Console.ReadLine();
                    if (qualification == null || qualification.Trim() == "")
                    {
                    Console.WriteLine("Квалификация не может быть пустой. Попробуйте снова.");
                    return;
                    }
              
                    Teacher newTeacher = new Teacher(name, surname, age, contactInfo, department, qualification);
                    teachers.Add(newTeacher);
                    Console.WriteLine($"Преподаватель успешно добавлен. Его ID: {newTeacher.GetId()}");
                }

                void ViewTeacherInfo()
                {
                    Console.WriteLine("Введите ID преподавателя для просмотра информации:");
                    int id = int.TryParse(Console.ReadLine(), out id) ? id : 0;
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
                    int teacherId = int.TryParse(Console.ReadLine(), out teacherId) ? teacherId : 0;
                    Teacher teacher = teachers.FirstOrDefault(t => t.GetId() == teacherId);
                    if (teacher != null)
                    {
                        Console.WriteLine("Введите ID курса для назначения преподавателя:");
                        int courseId = int.TryParse(Console.ReadLine(), out courseId) ? courseId : 0;
                        Course course = courses.FirstOrDefault(c => c.GetId() == courseId); 
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
                    if (courseName == null || courseName.Trim() == "")
                    {
                    Console.WriteLine("Название курса не может быть пустым. Попробуйте снова.");
                    return;
                    }
                    Console.WriteLine("Введите ID преподавателя для назначения на курс:");
                    int teacherId = int.TryParse(Console.ReadLine(), out teacherId) ? teacherId : 0;
                    Teacher teacher = teachers.FirstOrDefault(t => t.GetId() == teacherId);
                    if (teacher != null)
                    {
                        Course newCourse = new Course(courseName, teacher);
                        courses.Add(newCourse);
                        teacher.AssignCourse(newCourse);
                        Console.WriteLine($"Курс успешно создан. ID курса: {newCourse.GetId()}");
                    }
                    else
                    {
                        Console.WriteLine("Преподаватель с таким ID не найден.");
                    }
                }

                void ViewCourseInfo()
                {
                    Console.WriteLine("Введите ID курса для просмотра информации:");
                    int courseId = int.TryParse(Console.ReadLine(), out courseId) ? courseId : 0;
                    Course course = courses.FirstOrDefault(c => c.GetId() == courseId);
                    if (course != null)
                    {
                        course.DisplayCourseInfo();
                    }
                    else
                    {
                        Console.WriteLine("Курс с таким ID не найден.");
                    }
                }

                void ViewCourseStudents()
                {
                    Console.WriteLine("Введите ID курса для просмотра списка студентов:");
                    int courseId = int.TryParse(Console.ReadLine(), out courseId) ? courseId : 0;
                    Course course = courses.FirstOrDefault(c => c.GetId() == courseId);
                    if (course != null)
                    {
                        Console.WriteLine($"Список студентов курса {course.courseName}:");
                        foreach (var student in course.enrolledStudents)
                        {
                            Console.WriteLine($"- {student.name} {student.surname}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Курс с таким ID не найден.");
                    }
                }

                void ViewAllStudents()
                {
                    Console.WriteLine("Полный список студентов:");
                    foreach (var student in students)
                    {
                        student.DisplayInfo();
                    }
                }

                void ViewAllTeachers()
                {
                    Console.WriteLine("Полный список преподавателей:");
                    foreach (var teacher in teachers)
                    {
                        teacher.DisplayInfo();
                    }
                }

                void ViewAllCourses()
                {
                    Console.WriteLine("Полный список курсов:");
                    foreach (var course in courses)
                    {
                        course.DisplayCourseInfo();
                    }
                }

                while (true)
                {
                    Console.WriteLine("\nВыберите действие:");
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

                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1": AddStudent(); break;
                        case "2": ViewStudentInfo(); break;
                        case "3": EnrollStudentInCourse(); break;
                        case "4": ViewStudentCourses(); break;
                        case "5": AddTeacher(); break;
                        case "6": ViewTeacherInfo(); break;
                        case "7": AssignTeacherToCourse(); break;
                        case "8": AddCourse(); break;
                        case "9": ViewCourseInfo(); break;
                        case "10": ViewCourseStudents(); break;
                        case "11": ViewAllStudents(); break;
                        case "12": ViewAllTeachers(); break;
                        case "13": ViewAllCourses(); break;
                        case "0": return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
            }
    }
}
