using BLL;
using BLL.Entities;
using BLL.Interfaces;
using DAL;

namespace PL;

class Program
{
    static List<Person> people = new List<Person>();
    static PersonDataProvider personDataProvider;
    static StudentService studentService;

    static void Main()
    {
        personDataProvider = new PersonDataProvider("people.json");
        people = personDataProvider.GetAll();

        StudentDataProvider studentDataProvider =
            new StudentDataProvider("students.json");

        studentService = new StudentService(studentDataProvider);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nМеню");
            Console.WriteLine("1. Додати людей");
            Console.WriteLine("2. Показати всіх людей");
            Console.WriteLine("3. Знайти іноземних студентів-відмінників 1-го курсу");
            Console.WriteLine("4. Перевірити можливість отримання українського громадянства");
            Console.WriteLine("5. Student - Пірнати");
            Console.WriteLine("6. Singer - Навчатися грі на музичному інструменті");
            Console.WriteLine("0. Зберегти дані та вийти");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
                AddPeople();
            else if (choice == "2")
                ShowPeople();
            else if (choice == "3")
                ShowForeignExcellentStudents();
            else if (choice == "4")
                CheckCitizenship();
            else if (choice == "5")
                Dive();
            else if (choice == "6")
                StudyInstrument();
            else if (choice == "0")
            {
                SaveData();
                running = false;
            }
            else
                Console.WriteLine("Невірний пункт меню.");
        }
    }

    static void AddPeople()
    {
        Console.WriteLine("\nДодати людей");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Tailor");
        Console.WriteLine("3. Singer");
        Console.WriteLine("0. Повернутися до меню");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine() ?? "";

        if (choice == "0")
            return;

        Console.Write("Скільки людей додати: ");
        int count = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < count; i++)
        {
            if (choice == "1")
                AddStudent();
            else if (choice == "2")
                AddTailor();
            else if (choice == "3")
                AddSinger();
            else
            {
                Console.WriteLine("Невірний тип людини.");
                return;
            }
        }
    }

    static void AddStudent()
    {
        Console.WriteLine("\nStudent");

        int id = GetNextId();

        Console.Write("Ім'я: ");
        string firstName = Console.ReadLine()!;

        Console.Write("Прізвище: ");
        string lastName = Console.ReadLine()!;

        Console.Write("Курс: ");
        int course = int.Parse(Console.ReadLine()!);

        Console.Write("Студентський квиток: ");
        string studentTicket = Console.ReadLine()!;

        Console.Write("Середній бал: ");
        double averageMark = double.Parse(Console.ReadLine()!);

        Console.Write("Країна: ");
        string country = Console.ReadLine()!;

        Console.Write("Номер паспорта: ");
        string passportNumber = Console.ReadLine()!;

        Student student = new Student(
            id,
            firstName,
            lastName,
            course,
            studentTicket,
            averageMark,
            country,
            passportNumber);

        people.Add(student);

        Console.WriteLine("Student доданий.");
    }

    static void AddTailor()
    {
        int id = GetNextId();

        Console.Write("Ім'я: ");
        string firstName = Console.ReadLine()!;

        Console.Write("Прізвище: ");
        string lastName = Console.ReadLine()!;

        Tailor tailor = new Tailor(
            id,
            firstName,
            lastName);

        people.Add(tailor);

        Console.WriteLine("Tailor доданий.");
    }

    static void AddSinger()
    {
        int id = GetNextId();

        Console.Write("Ім'я: ");
        string firstName = Console.ReadLine()!;

        Console.Write("Прізвище: ");
        string lastName = Console.ReadLine()!;

        Console.Write("Музичний інструмент: ");
        string instrument = Console.ReadLine()!;

        Singer singer = new Singer(
            id,
            firstName,
            lastName,
            instrument);

        people.Add(singer);

        Console.WriteLine("Singer доданий.");
    }

    static void ShowPeople()
    {
        if (people.Count == 0)
        {
            Console.WriteLine("Список людей порожній.");
            return;
        }

        foreach (Person person in people)
            PrintPerson(person);
    }

    static void ShowForeignExcellentStudents()
    {
        List<Student> students =
            studentService.GetForeignExcellentFirstYearStudents();

        Console.WriteLine(
            $"Кількість іноземних студентів-відмінників 1-го курсу: {students.Count}");

        foreach (Student student in students)
            PrintPerson(student);
    }

    static void PrintPerson(Person person)
    {
        if (person is Student student)
        {
            Console.WriteLine(
                $"Student, Id: {student.Id}, FirstName: {student.FirstName}, " +
                $"LastName: {student.LastName}, Course: {student.Course}, " +
                $"StudentTicket: {student.StudentTicket}, " +
                $"AverageMark: {student.AverageMark}, " +
                $"Country: {student.Country}, " +
                $"PassportNumber: {student.PassportNumber}");
        }
        else if (person is Tailor tailor)
        {
            Console.WriteLine(
                $"Tailor, Id: {tailor.Id}, FirstName: {tailor.FirstName}, " +
                $"LastName: {tailor.LastName}");
        }
        else if (person is Singer singer)
        {
            Console.WriteLine(
                $"Singer, Id: {singer.Id}, FirstName: {singer.FirstName}, " +
                $"LastName: {singer.LastName}, Instrument: {singer.Instrument}");
        }
    }

    static void CheckCitizenship()
    {
        Console.Write("Скільки років людина проживає в Україні: ");
        int years = int.Parse(Console.ReadLine()!);

        if (studentService.CanObtainUkrainianCitizenship(years))
            Console.WriteLine("Можливість отримання українського громадянства є.");
        else
            Console.WriteLine("5 років проживання ще не виконано.");
    }

    static void Dive()
    {
        foreach (Person person in people)
        {
            if (person is Student student)
            {
                student.Dive();
                Console.WriteLine(
                    $"{student.FirstName} {student.LastName} тепер пірнає.");
                return;
            }
        }

        Console.WriteLine("Student не знайдений.");
    }

    static void StudyInstrument()
    {
        foreach (Person person in people)
        {
            if (person is Singer singer)
            {
                singer.StudyInstrument();
                Console.WriteLine(
                    $"{singer.FirstName} {singer.LastName} навчається грати на {singer.Instrument}.");
                return;
            }
        }

        Console.WriteLine("Singer не знайдений.");
    }

    static void SaveData()
    {
        personDataProvider.SaveAll(people);

        List<Student> students = new List<Student>();

        foreach (Person person in people)
        {
            if (person is Student student)
                students.Add(student);
        }

        StudentDataProvider studentDataProvider =
            new StudentDataProvider("students.json");

        studentDataProvider.SaveAll(students);

        Console.WriteLine("Дані збережено.");
    }

    static int GetNextId()
    {
        int maxId = 0;

        foreach (Person person in people)
        {
            if (person.Id > maxId)
                maxId = person.Id;
        }

        return maxId + 1;
    }
}