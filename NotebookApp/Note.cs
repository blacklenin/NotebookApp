using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookApp
{
    class Note
    {
        private static int countId = 1;

        public int Id { get; }
        public string Name;
        public string Surname;
        public long PhoneNumber;
        public string Country;

        //не являются обязательными
        public string Patronymic = "";
        public DateTime BirthDate;
        public string Organization = "Не указано";
        public string JobPosition = "Не указана";
        public string OtherNotes = "Отсутствуют";

        public Note()
        {
            this.Id = countId;
            countId++;
        }

        internal static void SetCountId(int param)
        {
            if (param >= 0)
                countId++;
            else
                countId--;
        }

        public override string ToString()
        {
            string date;
            if (BirthDate.Equals(new DateTime(1, 1, 1)))
                date = "Не указана";
            else
                date = BirthDate.ToString("dd.MM.yyyy");
            return Surname + " " + Name + " " + Patronymic + "\n"
                    + "Номер телефона: " + PhoneNumber + "\n"
                    + "Страна проживания: " + Country + "\n"
                    + "Дата рождения: " + date + "\n"
                    + "Место работы: " + Organization + "\n"
                    + "Должность: " + JobPosition + "\n"
                    + "Прочие заметки: " + OtherNotes;
        }
    }
}
