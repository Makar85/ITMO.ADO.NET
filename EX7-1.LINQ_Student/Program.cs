using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX7_1.LINQ_Student
{
    class Program
    {   
        static void Main(string[] args)
        {
            // создаем запрос
            IEnumerable<Student> studentQuery =
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                select student;
            // выполняем запрос
            foreach (Student student in studentQuery)
            {
                Console.WriteLine("{0}, {1}", student.Last, student.First);
                
            }
            Console.WriteLine("----------------------------------------------------------------");
            //---------------------------немеденное выполнение запросов--------------------------------

            int studentCount = (
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                select student).Count();

          //---------------------------то же что в предыдущем но с методом расширения---------------

            int studentCountW = students.Where(st => st.Scores[0] > 90 && st.Scores[3] < 80).Count();

            Console.WriteLine("Колличество студентов: {0},{1}", studentCount, studentCountW);
            Console.WriteLine("----------------------------------------------------------------");

            //-------------запрос с возвращением списка студентов-----------------------------------
            var studentList = (
                  from student in students
                  where student.Scores[0] > 90 && student.Scores[3] < 80
                  //Измените предложение orderby таким образом, чтобы оно сортировало результаты в
                  //обратном порядке по результату первого тестирования, от высшего к низшему показателю:
                  orderby student.Scores[0] descending
                  select student).ToList();
            //С помощью цикла отобразите список на экране:
            foreach (Student student in studentList)
            {
                //Измените строку форматирования WriteLine, чтобы видеть результаты тестирования:
                Console.WriteLine("{0},{1},{2}", student.Last, student.First, student.Scores[0]);
               
            }
            Console.WriteLine("----------------------------------------------------------------");

            //Запрос с предложением group создает последовательность групп, где каждая группа содержит "Key"
            //и последовательность, состоящую из всех членов этой группы.
            //Создайте новый запрос, который группирует учащихся по первой букве их фамилии в качестве ключа:
            var studentQuery2 =
                from student in students
                group student by student.Last[0];

            foreach (var studentGroup in studentQuery2)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (Student student in studentGroup)
                {
                    Console.WriteLine(" {0}, {1}",
                    student.Last, student.First);
                }
            }

            //--------------------------------------Ex 3-----------------------------------------------------

            Console.WriteLine("-----------------------------------Ex3---------------------------------------");
            
            var studentQuery3 =
                from student in students
                group student by student.Last[0];

            foreach (var groupOfStudents in studentQuery3)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine(" {0}, {1}",
                    student.Last, student.First);
                }
            }

            Console.WriteLine("-----------------------studentQuery4------------------------------------------");
            // расположение результата запроса в алфавитном порядке
            var studentQuery4 =
             from student in students
             group student by student.Last[0] into studentGroup
             orderby studentGroup.Key
             select studentGroup;

            foreach (var groupOfStudents in studentQuery4)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine(" {0}, {1}",
                    student.Last, student.First);
                }
            }

            Console.WriteLine("-----------------------studentQuery5 keyworld let------------------------------------------");
            //Ключевое слово let можно использовать для представления идентификатора для любого
            //результата выражения в выражении запроса.
            var studentQuery5 =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] +
                student.Scores[2] + student.Scores[3]
                where totalScore / 4 < student.Scores[0]
                select student.Last + " " + student.First;
            foreach (string s in studentQuery5)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("-----------------------studentQuery6 keyworld let------------------------------------------");
            //Создайте новый запрос в котором вычисляется общий результат для каждого Student в исходной последовательности,
            //а затем вызовите метод Average(), использующий результаты запроса для вычисления среднего балла класса. Обратите
            //внимание на круглые скобки вокруг выражения запроса.
            var studentQuery6 =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] +
                student.Scores[2] + student.Scores[3]
                select totalScore;
            double averageScore = studentQuery6.Average();
            
            Console.WriteLine("Class average score = {0}", averageScore);

            Console.WriteLine("-----------------------studentQuery7------------------------------------------");
            Console.WriteLine("Преобразование или проецирование в предложении select");
            // Создайте новый запрос, который возвращает последовательность строк (не Students):

            IEnumerable<string> studentQuery7 =
                from student in students
                where student.Last == "Garcia"
                select student.First;
            //В цикле foreach реализуйте перебор строк:
            Console.WriteLine("The Garcias in the class are:");
            foreach (string s in studentQuery7)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("-----------------------studentQuery8------------------------------------------");
            //Создайте последовательность Students, суммарный результат баллов которых больше среднего,
            //вместе с их StudentID, используйте анонимный тип в инструкции select:
            var studentQuery8 =
                from student in students
                let x = student.Scores[0] + student.Scores[1] +
                student.Scores[2] + student.Scores[3]
                where x > averageScore
                select new { id = student.ID, score = x };
            foreach (var item in studentQuery8)
            {
                Console.WriteLine("Student ID: {0}, Score: {1}", item.id, item.score);
            }

            Console.Read();
        }

        //В класс Program добавьте инициализированный список учащихся (создайте еще несколько студентов по обзазцу):
        static List<Student> students = new List<Student>
        {
            new Student {First="Svetlana",Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
            new Student {First="Claire", Last="O’Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
            new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
            new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
            new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
            new Student {First="Andrey",Last="Ivanov", ID=116, Scores= new List<int> {84, 59, 75, 92}},
            new Student {First="Joahn", Last="Petrov", ID=117, Scores= new List<int> {75, 67, 84, 71}},
            new Student {First="Anna", Last="Petrova", ID=118, Scores=new List<int> {98, 87, 74, 59}}
        };

        

    }

}
