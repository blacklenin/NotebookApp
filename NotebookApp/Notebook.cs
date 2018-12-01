using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotebookApp
{
    class Notebook
    {
        private static Dictionary<int, Note> notes = new Dictionary<int, Note>();

        public static void PrintMenu()
        {
            Console.WriteLine("Вы в главном меню записной книжки. Выберите номер действия, которое хотите совершить.");
            Console.WriteLine("1) Создать новую запись");
            Console.WriteLine("2) Редактировать созданную запись");
            Console.WriteLine("3) Удалить созданную запись");
            Console.WriteLine("4) Просмотреть учетную запись");
            Console.WriteLine("5) Просмотреть учетные записи, в краткой форме");
            Console.WriteLine("6) Завершить работу с записной книжкой");
        }

        public static void PrintFields()
        {
            Console.WriteLine("Вы берите номер поля, которое хотите отредакторовать или закончите редакитрование.");
            Console.WriteLine("1)Имя\n2)Фамилия\n3)Отчество\n4)Номер телефона\n5)Страна проживания");
            Console.WriteLine("6)Дата рождения\n7)Место работы\n8)Должность\n9)Прочие записки");
            Console.WriteLine("10) Закончить редактирование");
        }

        public static long ReadNumber()
        {
            long value;
            while (true)
            {
                try
                {
                    value = Convert.ToInt64(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Вы ввели что-то не то! Пожалуйста, повторите ввод!");
                    continue;
                }
                return value;
            }
        }

        public static string ReadString(string appeal)
        {
            Console.Clear();
            Console.WriteLine("Введите " + appeal);
            bool flag = false;
            string value;
            while (true)
            {
                value = Console.ReadLine();
                if (value.Equals(""))
                {
                    Console.WriteLine("Вы ничего не ввели! Пожалуйста, повторите ввод!");
                    continue;
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsLetter(value[i]))
                    {
                        Console.WriteLine("Некорректный ввод! Пожалуйста, повторите!");
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    flag = false;
                    continue;
                }
                return value;
            }
        }

        public static string ReadString(string appeal, string warning)
        {
            Console.Clear();
            Console.WriteLine("Введите " + appeal + " " + warning);
            string value;
            bool flag = false;
            while (true)
            {
                value = Console.ReadLine();
                if (value.Equals(""))
                {
                    return "";
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsLetter(value[i]))
                    {
                        Console.WriteLine("Некорректный ввод! Пожалуйста, повторите!");
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    flag = false;
                    continue;
                }
                return value;
            }
        }

        public static DateTime ReadDate(string appeal)
        {
            Console.Clear();
            Console.WriteLine("Введите " + appeal + ". Данное поле является не обязательным. Чтобы пропустить его нажмите Enter!");
            Console.WriteLine("Шаблон корректного ввода даты: dd.MM.yyyy");
            string tempString = "";
            DateTime value = new DateTime();
            while (true)
            {
                tempString = Console.ReadLine();
                if (tempString.Equals(""))
                    return new DateTime();
                try
                {
                    value = DateTime.Parse(tempString);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Некорректный ввод! Пожалуйста, повторите!");
                    continue;
                }
                if (value.Year < 2017 && value.Year > 1868)
                    break;
                Console.WriteLine("Ваш год рождения неправдоподобен! Пожалуйста, повторите ввод!");
            }
            return value;
        }

        public static Note CreateNote()
        {
            Console.Clear();
            Note note = new Note();
            long tempNumber;
            string patronymic, organization, jobPosition, otherNotes,
                    warning = "Данное поле является не обязательным. Чтобы пропустить его нажмите Enter!";
            note.Name = ReadString("ваше имя.");
            note.Surname = ReadString("вашу фамилию.");
            patronymic = ReadString("ваше отчество.", warning);
            if (!patronymic.Equals(""))
                note.Patronymic = patronymic;
            Console.Clear();
            Console.WriteLine("Введите ваш номер телефона.");
            while (true)
            {
                tempNumber = ReadNumber();
                if (tempNumber < 10000)
                    Console.WriteLine("Ваш номер слишком короткий! Повторите ввод!");
                else
                    break;
            }
            note.PhoneNumber = tempNumber;
            note.Country = ReadString("название страны, в которой вы проживаете.");
            note.BirthDate = ReadDate("дату вашего рождения.");
            organization = ReadString("ваше место работы.", warning);
            if (!organization.Equals(""))
                note.Organization = organization;
            jobPosition = ReadString("вашу должность.", warning);
            if (!jobPosition.Equals(""))
                note.JobPosition = jobPosition;
            Console.Clear();
            Console.WriteLine("Введите прочие заметки. " + warning);
            otherNotes = Console.ReadLine();
            if (!otherNotes.Equals(""))
                note.OtherNotes = otherNotes;
            Console.WriteLine("Для продолжения работы нажмите любую клавишу.");
            Console.ReadKey();
            return note;
        }

        public static void EditNote(int id)
        {
            Console.Clear();
            if (!notes.ContainsKey(id))
            {
                Console.WriteLine("Записи с таким id не существует! Для продолжения работы нажмите любую клавишу.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Выберите способ редактирования записи");
            Console.WriteLine("1) Редактировать запись полностью\n2) Редактировать отдельный пункт");
            while (true)
            {
                int numberAction = (int)ReadNumber();
                if (numberAction == 1)
                {
                    notes[id] = CreateNote();
                    Console.WriteLine("Запись успешно отредактирована. Для продолжения работы нажмите любую клавишу.");
                    Note.SetCountId(-1);
                    break;
                }
                else if (numberAction == 2)
                {
                    while (true)
                    {
                        Console.Clear();
                        PrintFields();
                        switch (ReadNumber())
                        {
                            case 1:
                                notes[id].Name = ReadString("ваше имя.");
                                break;
                            case 2:
                                notes[id].Surname = ReadString("вашу фамилию.");
                                break;
                            case 3:
                                notes[id].Patronymic = ReadString("ваше отчество.");
                                break;
                            case 4:
                                long tempNumber;
                                while (true)
                                {
                                    Console.WriteLine("Введите ваш номер телефона.");
                                    tempNumber = ReadNumber();
                                    if (tempNumber < 10000)
                                        Console.WriteLine("Ваш номер сликом короткий! Повторите ввод!");
                                    else
                                        break;
                                }
                                notes[id].PhoneNumber = tempNumber;
                                break;
                            case 5:
                                notes[id].Country = ReadString("название страны, в которой вы проживаете.");
                                break;
                            case 6:
                                notes[id].BirthDate = ReadDate("дату вашего рождения.");
                                break;
                            case 7:
                                notes[id].Organization = ReadString("ваше место работы.");
                                break;
                            case 8:
                                notes[id].JobPosition = ReadString("вашу должность.");
                                break;
                            case 9:
                                notes[id].OtherNotes = ReadString("прочие заметки.");
                                break;
                            case 10:
                                Console.WriteLine("Запись успешно отредактирована. Для продолжения работы нажмите любую клавишу.");
                                Console.ReadKey();
                                return;
                            default:
                                Console.WriteLine("Такой команды не существует! Повторите ввод!");
                                break;
                        }
                    }
                }
                else
                    Console.WriteLine("Такой команды не существует! Повторите ввод!");
            }
        }

        public static void DeleteNote(int id)
        {
            Console.Clear();
            if (notes.ContainsKey(id))
            {
                notes.Remove(id);
                Console.WriteLine("Запись успешно удалена! Для продолжения работы нажмите любую клавишу.");
            }
            else
            {
                Console.WriteLine("Значения с таким ключом нет! Для продолжения работы нажмите любую клавишу.");
            }
            Console.ReadKey();
        }

        public static void ReadNote(int id)
        {
            Console.Clear();
            if (!notes.ContainsKey(id))
            {
                Console.WriteLine("Записи с таким id не существует!");
            }
            else
            {
                Console.WriteLine(notes[id]);
            }
            Console.WriteLine("\nДля продолжения работы нажмите любую клавишу.");
            Console.ReadKey();
        }

        public static void ReadAllNotes()
        {
            Console.Clear();
            if (notes.Count == 0)
                Console.WriteLine("Записи отсутствуют!");
            else
            {
                foreach (KeyValuePair<int, Note> note in notes)
                {
                    Console.WriteLine(note.Key + " " + note.Value.Name + " " + note.Value.Surname + " " + note.Value.PhoneNumber);
                }
            }
            Console.WriteLine("Для продолжения работы нажмите любую клавишу.");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            long numberAction = 0;
            long id = 0;
            Console.WriteLine("\t\t\tДобро пожаловать в записную книжку!");
            while (true)
            {
                PrintMenu();
                numberAction = ReadNumber();
                switch (numberAction)
                {
                    case 1:
                        Note note = CreateNote();
                        Console.WriteLine("Ваша запись была создана с id = " + note.Id);
                        Thread.Sleep(1500);
                        notes.Add(note.Id, note);
                        break;
                    case 2:
                        if (notes.Count == 0)
                        {
                            Console.WriteLine("Записи отсутствуют! Для продолжения работы нажмите любую клавишу.");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Введите id записи, которую хотите отредактировать.");
                        id = ReadNumber();
                        EditNote((int)id);
                        break;
                    case 3:
                        if (notes.Count == 0)
                        {
                            Console.WriteLine("Записи отсутствуют! Для продолжения работы нажмите любую клавишу.");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Введите id записи, которую хотите удалить.");
                        id = ReadNumber();
                        DeleteNote((int)id);
                        break;
                    case 4:
                        if (notes.Count == 0)
                        {
                            Console.WriteLine("Записи отсутствуют! Для продолжения работы нажмите любую клавишу.");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Введите id записи, которую хотите вывести на экран.");
                        id = ReadNumber();
                        ReadNote((int)id);
                        break;
                    case 5:
                        ReadAllNotes();
                        break;
                    case 6:
                        Console.WriteLine("Прощайте!");
                        Thread.Sleep(2000);
                        return;
                    default:
                        Console.WriteLine("Такой команды не существует! Повторите ввод!");
                        Thread.Sleep(1500);
                        break;
                }
                Console.Clear();
            }
        }
    }
}

