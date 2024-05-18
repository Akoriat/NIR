using System;
using System.Collections.Generic;
using System.Linq;

namespace NIR.Recommender
{  
    public class RecommendationSystem
    {
        private double[,] ratingsMatrix; // Матрица оценок пользователей

        public RecommendationSystem(double[,] ratingsMatrix)
        {
            this.ratingsMatrix = ratingsMatrix;
        }

        // Метод для получения рекомендаций для заданного пользователя
        public List<int> GetRecommendations(int userId)
        {
            // 1. Найти пользователей, похожих на заданного
            List<int> similarUsers = FindSimilarUsers(userId);

            // 2. Найти предметы, которые пользователи-аналоги оценили высоко, а данный пользователь - нет
            List<int> recommendations = new List<int>();
            foreach (int similarUserId in similarUsers)
            {
                for (int itemId = 0; itemId < ratingsMatrix.GetLength(1); itemId++)
                {
                    // Если похожий пользователь оценил предмет высоко, а данный пользователь - нет
                    if (ratingsMatrix[similarUserId, itemId] > 0 && ratingsMatrix[userId, itemId] == 0)
                    {
                        recommendations.Add(itemId);
                    }
                }
            }

            // 3. Отсортировать рекомендации по оценке похожих пользователей
            recommendations.Sort((a, b) =>
                ratingsMatrix[similarUsers[0], b].CompareTo(ratingsMatrix[similarUsers[0], a]));

            return recommendations;
        }

        // Метод для поиска пользователей, похожих на заданного
        private List<int> FindSimilarUsers(int userId)
        {
            // Используем косинусное расстояние для определения похожести
            List<double> similarityScores = new List<double>();
            for (int otherUserId = 0; otherUserId < ratingsMatrix.GetLength(0); otherUserId++)
            {
                if (otherUserId != userId)
                {
                    similarityScores.Add(CalculateCosineSimilarity(userId, otherUserId));
                }
            }

            // Возвращаем список пользователей с наивысшим сходством
            List<int> similarUsers = similarityScores.Select((score, index) => new { Score = score, Index = index + 1 })
                                                        .OrderByDescending(x => x.Score)
                                                        .Take(5) // Выбираем 5 самых похожих
                                                        .Select(x => x.Index)
                                                        .ToList();
            return similarUsers;
        }

        // Метод для вычисления косинусного расстояния между двумя векторами
        private double CalculateCosineSimilarity(int userId1, int userId2)
        {
            double dotProduct = 0;
            double norm1 = 0;
            double norm2 = 0;
            for (int itemId = 0; itemId < ratingsMatrix.GetLength(1); itemId++)
            {
                dotProduct += ratingsMatrix[userId1, itemId] * ratingsMatrix[userId2, itemId];
                norm1 += Math.Pow(ratingsMatrix[userId1, itemId], 2);
                norm2 += Math.Pow(ratingsMatrix[userId2, itemId], 2);
            }
            return dotProduct / (Math.Sqrt(norm1) * Math.Sqrt(norm2));
        }
    }

    // Пример использования:
    public class Example
    {
        public static void Main(string[] args)
        {
            // Пример матрицы оценок
            double[,] ratings = {
            { 5, 3, 0, 4, 0 },
            { 0, 4, 5, 0, 1 },
            { 3, 0, 0, 2, 5 },
            { 4, 5, 0, 0, 0 },
            { 0, 1, 4, 3, 0 }
        };

            // Создаем объект RecommendationSystem
            RecommendationSystem system = new RecommendationSystem(ratings);

            // Получаем рекомендации для пользователя с ID 1
            List<int> recommendations = system.GetRecommendations(1);

            // Выводим рекомендации
            Console.WriteLine("Рекомендации для пользователя 1:");
            foreach (int itemId in recommendations)
            {
                Console.WriteLine("Предмет " + (itemId + 1));
            }
        }
    }
}
