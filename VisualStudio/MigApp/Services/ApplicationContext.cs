using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MigApp.Services
{
    internal class ApplicationContext
    {
         // Объявляем класс для подключения к БД

        private ApplicationContext()
        {
            // Пустой конструктор класса для Singletone
        }

        private static ApplicationContext instance; // Объявляем объект для экземпляра класса

        private static readonly object _lcok = new object(); // Объявление блокировки для потоков

        public static ApplicationContext GetInstance()
        {
            if (instance == null)
                lock (_lcok) // Блокируем потоки, если метод уже используется другим потоком
                    if (instance == null)
                        instance = new ApplicationContext();
            return instance;
        } // Получаем Sungletone экземпляр класса


        public enum WindowMode { Create, Edit, View, Archive }; // Режимы работы CR-окон


        #region Данные о текущем пользователе
        public async void SetCurrentUser(string login)
        {
            //// Устанавливаем логин пользователя
            //CurrentUser.Login = login;

            //// Получаем ID пользователя
            //CurrentUser.ID = await pgsql.GetUserID(login);

            //// Получаем ФИО пользователя
            //CurrentUser.FIO = await pgsql.ReqRef($"SELECT fio FROM \"Misc\".users_profiles_view WHERE users_profiles.id = {CurrentUser.ID}");

            //// Получаем ID роли
            //CurrentUser.RoleID = await pgsql.ReqRef($"SELECT role FROM \"Misc\".users_profiles WHERE id = {CurrentUser.ID}");

            //// Получем данные о роли
            //DataTable table = await pgsql.GetTable("*", "\"Misc\".roles", $"id = {CurrentUser.RoleID}");
            //if (table != null && table.Rows.Count > 0)
            //{
            //    DataRow row = table.Rows[0];

            //    // Установка параметров роли
            //    CurrentUser.RoleName = row["role_name"].ToString();
            //    CurrentUser.IsAdmin = row["administrator_permission"] == DBNull.Value ? null : (bool?)Convert.ToBoolean(row["administrator_permission"]);
            //    CurrentUser.CanEditTechnics = row["employees_permission"] == DBNull.Value ? null : (bool?)Convert.ToBoolean(row["employees_permission"]);
            //    CurrentUser.CanEditEmployees = row["technics_permission"] == DBNull.Value ? null : (bool?)Convert.ToBoolean(row["technics_permission"]);
            //    CurrentUser.CanEditFurniture = row["furniture_permission"] == DBNull.Value ? null : (bool?)Convert.ToBoolean(row["furniture_permission"]);
            //}

        }

        public _CurrentUser CurrentUser { get; set; } = new _CurrentUser(); // Объявление класса CurrentUser для взаимодействия с данными в классе

        public class _CurrentUser
        {
            public string Login { get; set; } = "";
            public string ID { get; set; } = "";
            public string FIO { get; set; } = "";
            public string RoleID { get; set; } = "";
            public string RoleName { get; set; } = "";
            public bool? IsAdmin { get; set; } = null;
            public bool? CanEditTechnics { get; set; } = null;
            public bool? CanEditEmployees { get; set; } = null;
            public bool? CanEditFurniture { get; set; } = null;
        } // Класс хранящий в себе данные о текущем пользователе
        #endregion
    }
}
