using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingGame
{
    class Enemy
    {
        public Enemy(int health)
        {
            m_MaxHealth = health;
            m_CurrentHealth = m_MaxHealth;
        }

        public int GetCurrentHealth()
        {
            return m_CurrentHealth;
        }

        public void ModifyCurrentHealth(int mod)
        {
            m_CurrentHealth += mod;
        }

        public int GetMaxHealth()
        {
            return m_MaxHealth;
        }

        public bool isAlive()
        {
            return (m_CurrentHealth > 0);
        }

        int m_MaxHealth;
        int m_CurrentHealth;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Program instance = new Program();
            Stack<string> offenseWords = new Stack<string>();
            Stack<string> defenseWords = new Stack<string>();
            Enemy currentEnemy;
            bool isPlayerAlive = true;

            InitializeWords(offenseWords, "OffenseWords.txt");
            InitializeWords(defenseWords, "DefenseWords.txt");

            currentEnemy = SpawnEnemy();

            while (isPlayerAlive)
            {
                CombatStep(currentEnemy, GetWordFromList(offenseWords), ref isPlayerAlive);

                if(!currentEnemy.isAlive())
                {
                    currentEnemy = SpawnEnemy();
                }
            }
        }

        static Enemy SpawnEnemy()
        {
            Console.WriteLine("New Enemy Appeared!");
            Enemy enemy = new Enemy(3);
            return enemy;
        }

        static void CombatStep(Enemy enemy, string word, ref bool isAlive)
        {
            StringBuilder display = new StringBuilder();
            display.Append("Enemy's Health: ").Append(enemy.GetCurrentHealth()).Append("/").Append(enemy.GetMaxHealth()).AppendLine().Append(word).AppendLine();
            Console.WriteLine(display.ToString());

            if(Console.ReadLine() == word)
            {
                enemy.ModifyCurrentHealth(-1);
            }

            else
            {
                KillPlayer(ref isAlive);
            }
        }

        static string GetWordFromList(Stack<string> data)
        {
            return data.ElementAt(GetRandomNumber(0, data.Count)); //returns a random element from the list given
        }

        static int GetRandomNumber(int min, int max)
        {
            Random num = new Random();
            return num.Next(min, max);
        }

        static void KillPlayer(ref bool isAlive)
        {
            Console.WriteLine("You have died! Game Over");
            isAlive = false;
        }

        static void InitializeWords(Stack<string> container, string path)
        {
            string[] temp = LoadTextToStrings(path);

            for (int i = 0; i < temp.Count(); i++)
            {
                container.Push(temp[i]);
            }
        }

       static string[] LoadTextToStrings(string path)
        {
            return System.IO.File.ReadAllText(path).Split(',');
        }
    }
}
