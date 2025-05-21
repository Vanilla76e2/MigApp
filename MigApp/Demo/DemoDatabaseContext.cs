using Microsoft.EntityFrameworkCore;
using MigApp.Core.Enums;
using MigApp.Demo.Helpers;
using MigApp.Infrastructure.Data.Entities;

namespace MigApp.Demo
{
    public partial class DemoDatabaseContext : DbContext
    {
        public DemoDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Cctv> Cctvs =>
            DbSetMockHelper.CreateMockDbSet(new List<Cctv>
            {
                new Cctv
                {
                    Id = 1,
                    InventoryNumber = 100000001,
                    Model = "Hikvision DS-2CD2347G2-LU",
                    Location = "Главный вход",
                    Resolution = "4MP",
                    Ip = "10.0.0.10",
                    Street = true,
                    Microphone = true,
                    Comment = "Основной вход в здание",
                    Deleted = false,
                    DelDate = null
                },
                new Cctv
                {
                    Id = 2,
                    InventoryNumber = 100000002,
                    Model = "Dahua IPC-HDW2831T-AS-S2",
                    Location = "Парковка, сектор A",
                    Resolution = "8MP",
                    Ip = "10.0.0.11",
                    Street = true,
                    Microphone = false,
                    Comment = "Наблюдение за парковкой",
                    Deleted = false,
                    DelDate = null
                },
                new Cctv
                {
                    Id = 3,
                    InventoryNumber = 100000003,
                    Model = "Axis M3067-P",
                    Location = "Холл 1-го этажа",
                    Resolution = "5MP",
                    Ip = "10.0.0.12",
                    Street = false,
                    Microphone = true,
                    Comment = "Вид на ресепшен",
                    Deleted = false,
                    DelDate = null
                },
                new Cctv
                {
                    Id = 4,
                    InventoryNumber = 100000004,
                    Model = "Hikvision DS-2CD1023G0E-I",
                    Location = "Склад, зона приёмки",
                    Resolution = "2MP",
                    Ip = "10.0.0.13",
                    Street = false,
                    Microphone = false,
                    Comment = "Контроль разгрузки",
                    Deleted = false,
                    DelDate = null
                },
                new Cctv
                {
                    Id = 5,
                    InventoryNumber = 100000005,
                    Model = "Dahua IPC-HFW2431T-ZS-S2",
                    Location = "Технический коридор",
                    Resolution = "4MP",
                    Ip = "10.0.0.14",
                    Street = false,
                    Microphone = false,
                    Comment = "Контроль доступа в серверную",
                    Deleted = false,
                    DelDate = null
                },
                new Cctv
                {
                    Id = 6,
                    InventoryNumber = 100000006,
                    Model = "Hikvision DS-2CD2T46G2-ISU",
                    Location = "Черный вход",
                    Resolution = "4MP",
                    Ip = "10.0.0.15",
                    Street = true,
                    Microphone = true,
                    Comment = "Используется для грузовой зоны",
                    Deleted = false,
                    DelDate = null
                },
                new Cctv
                {
                    Id = 7,
                    InventoryNumber = 100000007,
                    Model = "Axis P1448-LE",
                    Location = "Проходная B",
                    Resolution = "8MP",
                    Ip = "10.0.0.16",
                    Street = true,
                    Microphone = true,
                    Comment = "Основной контроль проходной B",
                    Deleted = true,
                    DelDate = new DateOnly(2025, 01, 15)
                }
            }).Object;
        public virtual DbSet<Computer> Computers =>
            DbSetMockHelper.CreateMockDbSet(new List<Computer>
            {
                new Computer
                {
                    Id = 1,
                    InventoryNumber = 200000001,
                    Name = "WS-Ivanov-01",
                    Ip = IpToLong("10.0.0.100"),
                    EmployeeId = 1,
                    OperatingSystem = "Windows 11 Pro",
                    Comment = "Основной ПК администратора",
                    Deleted = false,
                    DelDate = null,
                    Employee = Employees.FirstOrDefault(e => e.Id == 1),
                    ComputersComponents = ComputersComponents.Where(c => c.ComputerId == 1).ToList(),
                    ComputersDevices = ComputersDevices.Where(d => d.ComputerId == 1).ToList(),
                    ComputersServiceHistories = ComputersServiceHistories.Where(s => s.ComputerId == 1).ToList()
                },
                new Computer
                {
                    Id = 2,
                    InventoryNumber = 200000002,
                    Name = "WS-Petrov-01",
                    Ip = IpToLong("10.0.0.101"),
                    EmployeeId = 2,
                    OperatingSystem = "Windows 10 Pro",
                    Comment = "ПК главного бухгалтера",
                    Deleted = false,
                    DelDate = null,
                    Employee = Employees.FirstOrDefault(e => e.Id == 2),
                    ComputersComponents = ComputersComponents.Where(c => c.ComputerId == 2).ToList(),
                    ComputersDevices = ComputersDevices.Where(d => d.ComputerId == 2).ToList(),
                    ComputersServiceHistories = ComputersServiceHistories.Where(s => s.ComputerId == 2).ToList()
                },
                new Computer
                {
                    Id = 3,
                    InventoryNumber = 200000003,
                    Name = "WS-Sidorova-01",
                    Ip = IpToLong("10.0.0.102"),
                    EmployeeId = 3,
                    OperatingSystem = "Windows 10 Home",
                    Comment = "Рабочая станция отдела кадров",
                    Deleted = false,
                    DelDate = null,
                    Employee = Employees.FirstOrDefault(e => e.Id == 3),
                    ComputersComponents = ComputersComponents.Where(c => c.ComputerId == 3).ToList(),
                    ComputersDevices = ComputersDevices.Where(d => d.ComputerId == 3).ToList(),
                    ComputersServiceHistories = ComputersServiceHistories.Where(s => s.ComputerId == 3).ToList()
                },
                new Computer
                {
                    Id = 4,
                    InventoryNumber = 200000004,
                    Name = "WS-Storage-01",
                    Ip = IpToLong("10.0.0.103"),
                    EmployeeId = null,
                    OperatingSystem = "Ubuntu Server 22.04",
                    Comment = "Свободный ПК на складе",
                    Deleted = false,
                    DelDate = null,
                    Employee = null,
                    ComputersComponents = ComputersComponents.Where(c => c.ComputerId == 4).ToList(),
                    ComputersDevices = ComputersDevices.Where(d => d.ComputerId == 4).ToList(),
                    ComputersServiceHistories = ComputersServiceHistories.Where(s => s.ComputerId == 4).ToList()
                },
                new Computer
                {
                    Id = 5,
                    InventoryNumber = 200000005,
                    Name = "WS-Archive-01",
                    Ip = IpToLong("10.0.0.104"),
                    EmployeeId = null,
                    OperatingSystem = "Windows 7 Pro",
                    Comment = "Архивный ПК, подлежит списанию",
                    Deleted = true,
                    DelDate = new DateOnly(2024, 10, 10),
                    Employee = null,
                    ComputersComponents = ComputersComponents.Where(c => c.ComputerId == 5).ToList(),
                    ComputersDevices = ComputersDevices.Where(d => d.ComputerId == 5).ToList(),
                    ComputersServiceHistories = ComputersServiceHistories.Where(s => s.ComputerId == 5).ToList()
                }
            }).Object;

        public virtual DbSet<ComputersComponent> ComputersComponents =>
            DbSetMockHelper.CreateMockDbSet(new List<ComputersComponent>
            {
                new ComputersComponent
                {
                    Id = 1,
                    ComputerId = 1, // WS-Ivanov-01
                    ComponentName = "Процессор",
                    ComponentInvnum = "CPU-0001",
                    ComponentSpecifies = "Intel Core i7-12700K",
                    Computer = Computers.First(c => c.Id == 1)
                },
                new ComputersComponent
                {
                    Id = 2,
                    ComputerId = 1,
                    ComponentName = "Оперативная память",
                    ComponentInvnum = "RAM-0001",
                    ComponentSpecifies = "32GB DDR4 3200MHz",
                    Computer = Computers.First(c => c.Id == 1)
                },
                new ComputersComponent
                {
                    Id = 3,
                    ComputerId = 2, // WS-Petrov-01
                    ComponentName = "Жесткий диск",
                    ComponentInvnum = "HDD-0002",
                    ComponentSpecifies = "1TB HDD Seagate",
                    Computer = Computers.First(c => c.Id == 2)
                },
                new ComputersComponent
                {
                    Id = 4,
                    ComputerId = 2,
                    ComponentName = "Оперативная память",
                    ComponentInvnum = "RAM-0002",
                    ComponentSpecifies = "16GB DDR4 2666MHz",
                    Computer = Computers.First(c => c.Id == 2)
                },
                new ComputersComponent
                {
                    Id = 5,
                    ComputerId = 3, // WS-Sidorova-01
                    ComponentName = "Процессор",
                    ComponentInvnum = "CPU-0003",
                    ComponentSpecifies = "Intel Core i5-10400",
                    Computer = Computers.First(c => c.Id == 3)
                },
                new ComputersComponent
                {
                    Id = 6,
                    ComputerId = 4, // WS-Storage-01
                    ComponentName = "Жесткий диск",
                    ComponentInvnum = null,
                    ComponentSpecifies = "500GB SSD Kingston",
                    Computer = Computers.First(c => c.Id == 4)
                },
                new ComputersComponent
                {
                    Id = 7,
                    ComputerId = 5, // WS-Archive-01 (Удалённый)
                    ComponentName = "Процессор",
                    ComponentInvnum = "CPU-ARCH-001",
                    ComponentSpecifies = "Intel Pentium G2020",
                    Computer = Computers.First(c => c.Id == 5)
                }
            }).Object;

        public virtual DbSet<ComputersDevice> ComputersDevices =>
            DbSetMockHelper.CreateMockDbSet(new List<ComputersDevice>
            {
                new ComputersDevice
                {
                    Id = 1,
                    ComputerId = 1, // WS-Ivanov-01
                    DeviceType = "Монитор",
                    DeviceId = 101,
                    DeviceName = "Samsung S24F350",
                    DeviceInvnum = "MON-0001",
                    DeviceSpecification = "24\" FHD 1920x1080, IPS",
                    DeviceComment = "Основной монитор",
                    Computer = Computers.First(c => c.Id == 1)
                },
                new ComputersDevice
                {
                    Id = 2,
                    ComputerId = 1,
                    DeviceType = "Клавиатура",
                    DeviceId = 102,
                    DeviceName = "Logitech K380",
                    DeviceInvnum = "KEY-0001",
                    DeviceSpecification = "Беспроводная, Bluetooth",
                    DeviceComment = null,
                    Computer = Computers.First(c => c.Id == 1)
                },
                new ComputersDevice
                {
                    Id = 3,
                    ComputerId = 2, // WS-Petrov-01
                    DeviceType = "Монитор",
                    DeviceId = 103,
                    DeviceName = "Dell P2419H",
                    DeviceInvnum = "MON-0002",
                    DeviceSpecification = "24\" FHD 1920x1080, IPS",
                    DeviceComment = "Установлен вертикально",
                    Computer = Computers.First(c => c.Id == 2)
                },
                new ComputersDevice
                {
                    Id = 4,
                    ComputerId = 2,
                    DeviceType = "Принтер",
                    DeviceId = 104,
                    DeviceName = "HP LaserJet Pro M404dn",
                    DeviceInvnum = "PRN-0001",
                    DeviceSpecification = "Монохромный, сетевой",
                    DeviceComment = "Личный принтер в бухгалтерии",
                    Computer = Computers.First(c => c.Id == 2)
                },
                new ComputersDevice
                {
                    Id = 5,
                    ComputerId = 3, // WS-Sidorova-01
                    DeviceType = "Монитор",
                    DeviceId = 105,
                    DeviceName = "Acer V226HQL",
                    DeviceInvnum = "MON-0003",
                    DeviceSpecification = "22\" FHD 1920x1080",
                    DeviceComment = null,
                    Computer = Computers.First(c => c.Id == 3)
                },
                new ComputersDevice
                {
                    Id = 6,
                    ComputerId = 4, // WS-Storage-01
                    DeviceType = "Сканер",
                    DeviceId = 106,
                    DeviceName = "Canon LIDE 300",
                    DeviceInvnum = "SCN-0001",
                    DeviceSpecification = "USB, до A4",
                    DeviceComment = "Используется при приёмке товаров",
                    Computer = Computers.First(c => c.Id == 4)
                },
                new ComputersDevice
                {
                    Id = 7,
                    ComputerId = 5, // WS-Archive-01 (удалённый)
                    DeviceType = "Монитор",
                    DeviceId = 107,
                    DeviceName = "LG Flatron E2240",
                    DeviceInvnum = "MON-ARCH-001",
                    DeviceSpecification = "21.5\" FHD 1920x1080",
                    DeviceComment = "Старый монитор, в архиве",
                    Computer = Computers.First(c => c.Id == 5)
                }
            }).Object;

        public virtual DbSet<ComputersServiceHistory> ComputersServiceHistories =>
            DbSetMockHelper.CreateMockDbSet(new List<ComputersServiceHistory>
            {
                new ComputersServiceHistory
                {
                    Id = 1,
                    ComputerId = 1, // WS-Ivanov-01
                    ServiceDate = new DateOnly(2024, 12, 5),
                    Servicer = "Иванов Иван Иванович",
                    ServiceDescription = "Замена термопасты и чистка системы охлаждения.",
                    Computer = Computers.First(c => c.Id == 1)
                },
                new ComputersServiceHistory
                {
                    Id = 2,
                    ComputerId = 2, // WS-Petrov-01
                    ServiceDate = new DateOnly(2024, 11, 20),
                    Servicer = "Сидорова Анна Сергеевна",
                    ServiceDescription = "Установка нового жесткого диска 1TB HDD.",
                    Computer = Computers.First(c => c.Id == 2)
                },
                new ComputersServiceHistory
                {
                    Id = 3,
                    ComputerId = 2,
                    ServiceDate = new DateOnly(2025, 1, 10),
                    Servicer = "Егоров Дмитрий Валерьевич",
                    ServiceDescription = "Обновление операционной системы до Windows 10 Pro.",
                    Computer = Computers.First(c => c.Id == 2)
                },
                new ComputersServiceHistory
                {
                    Id = 4,
                    ComputerId = 4, // WS-Storage-01
                    ServiceDate = new DateOnly(2024, 10, 15),
                    Servicer = "Зайцева Мария Владимировна",
                    ServiceDescription = "Профилактическая проверка, замена SSD.",
                    Computer = Computers.First(c => c.Id == 4)
                },
                new ComputersServiceHistory
                {
                    Id = 5,
                    ComputerId = 5, // WS-Archive-01
                    ServiceDate = new DateOnly(2023, 5, 5),
                    Servicer = "Неизвестно",
                    ServiceDescription = "Компьютер списан, дальнейшее обслуживание не требуется.",
                    Computer = Computers.First(c => c.Id == 5)
                }
            }).Object;

        public virtual DbSet<Department> Departments =>
            DbSetMockHelper.CreateMockDbSet(new List<Department>
            {
                new Department
                {
                    Id = 1,
                    DepartmentName = "Отдел информационных технологий",
                    Employees = Employees.Where(e => e.DepartmentId == 1).ToList()
                },
                new Department
                {
                    Id = 2,
                    DepartmentName = "Бухгалтерия",
                    Employees = Employees.Where(e => e.DepartmentId == 2).ToList()
                },
                new Department
                {
                    Id = 3,
                    DepartmentName = "Отдел кадров",
                    Employees = Employees.Where(e => e.DepartmentId == 3).ToList()
                },
                new Department
                {
                    Id = 4,
                    DepartmentName = "Логистический отдел",
                    Employees = Employees.Where(e => e.DepartmentId == 4).ToList()
                },
                new Department
                {
                    Id = 5,
                    DepartmentName = "Отдел маркетинга",
                    Employees = Employees.Where(e => e.DepartmentId == 5).ToList()
                },
                new Department
                {
                    Id = 6,
                    DepartmentName = "Юридический отдел",
                    Employees = new List<Employee>() // Пока без сотрудников
                },
                new Department
                {
                    Id = 7,
                    DepartmentName = "Отдел безопасности",
                    Employees = Employees.Where(e => e.DepartmentId == 7).ToList()
                }
            }).Object;

        public virtual DbSet<Employee> Employees =>
            DbSetMockHelper.CreateMockDbSet(new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Fio = "Иванов Иван Иванович",
                    DepartmentId = 1, // ИТ
                    Workplace = "101А",
                    Deleted = false,
                    DelDate = null,
                    Comment = "Администратор систем",
                    PhoneNumber = "+7 (900) 123-45-67",
                    Department = Departments.First(d => d.Id == 1)
                },
                new Employee
                {
                    Id = 2,
                    Fio = "Петров Пётр Петрович",
                    DepartmentId = 2, // Бухгалтерия
                    Workplace = "202Б",
                    Deleted = false,
                    DelDate = null,
                    Comment = "Главный бухгалтер",
                    PhoneNumber = "+7 (901) 234-56-78",
                    Department = Departments.First(d => d.Id == 2)
                },
                new Employee
                {
                    Id = 3,
                    Fio = "Сидорова Анна Сергеевна",
                    DepartmentId = 3, // Кадры
                    Workplace = "303",
                    Deleted = false,
                    DelDate = null,
                    Comment = "Специалист по кадрам",
                    PhoneNumber = "+7 (902) 345-67-89",
                    Department = Departments.First(d => d.Id == 3)
                },
                new Employee
                {
                    Id = 4,
                    Fio = "Морозов Алексей Николаевич",
                    DepartmentId = 4, // Логистика
                    Workplace = "Склад №1",
                    Deleted = false,
                    DelDate = null,
                    Comment = "Ответственный за отгрузку",
                    PhoneNumber = "+7 (903) 456-78-90",
                    Department = Departments.First(d => d.Id == 4)
                },
                new Employee
                {
                    Id = 5,
                    Fio = "Егоров Дмитрий Валерьевич",
                    DepartmentId = 1, // ИТ
                    Workplace = "102",
                    Deleted = true,
                    DelDate = new DateOnly(2024, 11, 15),
                    Comment = "Уволен, переход на удалёнку не согласован",
                    PhoneNumber = null,
                    Department = Departments.First(d => d.Id == 1)
                },
                new Employee
                {
                    Id = 6,
                    Fio = "Зайцева Мария Владимировна",
                    DepartmentId = 5, // Маркетинг
                    Workplace = "401",
                    Deleted = false,
                    DelDate = null,
                    Comment = "Ответственная за PR",
                    PhoneNumber = "+7 (904) 567-89-01",
                    Department = Departments.First(d => d.Id == 5)
                },
                new Employee
                {
                    Id = 7,
                    Fio = "Волков Николай Петрович",
                    DepartmentId = 7, // Безопасность
                    Workplace = "Охрана",
                    Deleted = false,
                    DelDate = null,
                    Comment = "Начальник службы безопасности",
                    PhoneNumber = "+7 (905) 678-90-12",
                    Department = Departments.First(d => d.Id == 7)
                }
            }).Object;

        public virtual DbSet<Favourite> Favourites =>
            DbSetMockHelper.CreateMockDbSet(new List<Favourite>
            {
                new Favourite
                {
                    FavedDate = DateTime.Now.AddDays(-5),
                    UserId = 1, // admin
                    TableName = "Computer",
                    RowId = 1, // WS-Ivanov-01
                    User = UsersProfiles.First(u => u.Id == 1)
                },
                new Favourite
                {
                    FavedDate = DateTime.Now.AddDays(-3),
                    UserId = 2, // petrov
                    TableName = "Orgtechnic",
                    RowId = 1, // Принтер HP LaserJet
                    User = UsersProfiles.First(u => u.Id == 2)
                },
                new Favourite
                {
                    FavedDate = DateTime.Now.AddDays(-2),
                    UserId = 3, // sidorova
                    TableName = "Monitor",
                    RowId = 2, // Dell P2419H
                    User = UsersProfiles.First(u => u.Id == 3)
                },
                new Favourite
                {
                    FavedDate = DateTime.Now.AddDays(-1),
                    UserId = 1, // admin
                    TableName = "Router",
                    RowId = 1, // MainRouter
                    User = UsersProfiles.First(u => u.Id == 1)
                },
                new Favourite
                {
                    FavedDate = DateTime.Now,
                    UserId = 5, // egorov
                    TableName = "Tablet",
                    RowId = 2, // Samsung Galaxy Tab S9
                    User = UsersProfiles.First(u => u.Id == 5)
                }
            }).Object;

        public virtual DbSet<FavouriteView> FavouriteViews =>
            DbSetMockHelper.CreateMockDbSet(new List<FavouriteView>
            {
                new FavouriteView
                {
                    FavedDate = DateTime.Now.AddDays(-5),
                    TableName = "Computer",
                    ItemDescription = "WS-Ivanov-01 (Инв. №200000001)",
                    Comment = "Основной ПК администратора",
                    UserId = 1 // admin
                },
                new FavouriteView
                {
                    FavedDate = DateTime.Now.AddDays(-3),
                    TableName = "Orgtechnic",
                    ItemDescription = "Принтер HP LaserJet Pro M404dn (Инв. №500000001)",
                    Comment = "Принтер в бухгалтерии",
                    UserId = 2 // petrov
                },
                new FavouriteView
                {
                    FavedDate = DateTime.Now.AddDays(-2),
                    TableName = "Monitor",
                    ItemDescription = "Dell P2419H (Инв. №400000002)",
                    Comment = "Монитор с вертикальной установкой",
                    UserId = 3 // sidorova
                },
                new FavouriteView
                {
                    FavedDate = DateTime.Now.AddDays(-1),
                    TableName = "Router",
                    ItemDescription = "MainRouter (Модель: MikroTik RB4011)",
                    Comment = "Главный маршрутизатор в серверной",
                    UserId = 1 // admin
                },
                new FavouriteView
                {
                    FavedDate = DateTime.Now,
                    TableName = "Tablet",
                    ItemDescription = "Samsung Galaxy Tab S9 (Инв. №800000002)",
                    Comment = "Используется на складе",
                    UserId = 5 // egorov
                }
            }).Object;

        public virtual DbSet<GenericDevice> GenericDevices =>
            DbSetMockHelper.CreateMockDbSet(new List<GenericDevice>
            {
                new GenericDevice
                {
                    Id = 101,
                    DeviceType = "Монитор",
                    DeviceName = "Samsung S24F350",
                    DeviceInvnum = "MON-0001",
                    DeviceSpecification = "24\" FHD 1920x1080, IPS",
                    DeviceComment = "Основной монитор для офиса"
                },
                new GenericDevice
                {
                    Id = 102,
                    DeviceType = "Клавиатура",
                    DeviceName = "Logitech K380",
                    DeviceInvnum = "KEY-0001",
                    DeviceSpecification = "Беспроводная, Bluetooth",
                    DeviceComment = "Компактная модель для ИТ отдела"
                },
                new GenericDevice
                {
                    Id = 103,
                    DeviceType = "Монитор",
                    DeviceName = "Dell P2419H",
                    DeviceInvnum = "MON-0002",
                    DeviceSpecification = "24\" FHD 1920x1080, IPS",
                    DeviceComment = "С регулировкой по высоте"
                },
                new GenericDevice
                {
                    Id = 104,
                    DeviceType = "Принтер",
                    DeviceName = "HP LaserJet Pro M404dn",
                    DeviceInvnum = "PRN-0001",
                    DeviceSpecification = "Монохромный, сетевой",
                    DeviceComment = "Личный принтер для бухгалтерии"
                },
                new GenericDevice
                {
                    Id = 105,
                    DeviceType = "Монитор",
                    DeviceName = "Acer V226HQL",
                    DeviceInvnum = "MON-0003",
                    DeviceSpecification = "22\" FHD 1920x1080",
                    DeviceComment = "Экономичная модель для кадров"
                },
                new GenericDevice
                {
                    Id = 106,
                    DeviceType = "Сканер",
                    DeviceName = "Canon LIDE 300",
                    DeviceInvnum = "SCN-0001",
                    DeviceSpecification = "USB, до A4",
                    DeviceComment = "Используется на складе для приёмки"
                },
                new GenericDevice
                {
                    Id = 107,
                    DeviceType = "Монитор",
                    DeviceName = "LG Flatron E2240",
                    DeviceInvnum = "MON-ARCH-001",
                    DeviceSpecification = "21.5\" FHD 1920x1080",
                    DeviceComment = "Устаревший монитор, списан"
                }
            }).Object;

        public virtual DbSet<IpAddressInfo> IpAddressInfos =>
            DbSetMockHelper.CreateMockDbSet(new List<IpAddressInfo>
            {
                new IpAddressInfo
                {
                    Id = 1,
                    Ipaddress = IpToLong("10.0.0.10"),
                    Device = "Компьютер",
                    Inventorynumber = "200000001",
                    Devicename = "WS-Ivanov-01",
                    Comment = "Рабочее место администратора"
                },
                new IpAddressInfo
                {
                    Id = 2,
                    Ipaddress = IpToLong("10.0.0.11"),
                    Device = "Принтер",
                    Inventorynumber = "500000001",
                    Devicename = "HP LaserJet Pro M404dn",
                    Comment = "Принтер в бухгалтерии"
                },
                new IpAddressInfo
                {
                    Id = 3,
                    Ipaddress = IpToLong("10.0.0.12"),
                    Device = "Роутер",
                    Inventorynumber = "600000001",
                    Devicename = "MainRouter",
                    Comment = "Главный маршрутизатор в серверной"
                },
                new IpAddressInfo
                {
                    Id = 4,
                    Ipaddress = IpToLong("10.0.1.10"),
                    Device = "Свитч",
                    Inventorynumber = "700000001",
                    Devicename = "CoreSwitch",
                    Comment = "L2-коммутатор в серверной"
                },
                new IpAddressInfo
                {
                    Id = 5,
                    Ipaddress = IpToLong("10.0.2.15"),
                    Device = "Камера",
                    Inventorynumber = "100000002",
                    Devicename = "Dahua IPC-HDW2831T-AS-S2",
                    Comment = "Камера на парковке, сектор A"
                }
            }).Object;

        public virtual DbSet<Laptop> Laptops =>
            DbSetMockHelper.CreateMockDbSet(new List<Laptop>
            {
                new Laptop
                {
                    Id = 1,
                    InventoryNumber = 300000001,
                    Model = "Lenovo ThinkPad X1 Carbon Gen 9",
                    SerialNumber = "SN-THINK-001",
                    EmployeeId = 1, // Иванов Иван Иванович
                    Diagonal = 14.0f,
                    Resolution = "1920x1200",
                    OperatingSystem = "Windows 11 Pro",
                    Processor = "Intel Core i7-1165G7",
                    Ram = "16GB LPDDR4x",
                    Drive = "512GB SSD NVMe",
                    Other = "WebCam, Fingerprint, LTE",
                    Comment = "Используется для удалённой работы",
                    Deleted = false,
                    DelDate = null,
                    Employee = Employees.FirstOrDefault(e => e.Id == 1)
                },
                new Laptop
                {
                    Id = 2,
                    InventoryNumber = 300000002,
                    Model = "Apple MacBook Pro 14",
                    SerialNumber = "SN-MBP-002",
                    EmployeeId = 6, // Зайцева Мария Владимировна (Маркетинг)
                    Diagonal = 14.2f,
                    Resolution = "3024x1964",
                    OperatingSystem = "macOS Sonoma",
                    Processor = "Apple M2 Pro",
                    Ram = "16GB Unified",
                    Drive = "1TB SSD",
                    Other = "Touch ID, Retina Display",
                    Comment = "Используется для презентаций и работы с графикой",
                    Deleted = false,
                    DelDate = null,
                    Employee = Employees.FirstOrDefault(e => e.Id == 6)
                },
                new Laptop
                {
                    Id = 3,
                    InventoryNumber = 300000003,
                    Model = "HP EliteBook 850 G8",
                    SerialNumber = "SN-HP-003",
                    EmployeeId = null, // Пока не выдан
                    Diagonal = 15.6f,
                    Resolution = "1920x1080",
                    OperatingSystem = "Windows 10 Pro",
                    Processor = "Intel Core i5-1135G7",
                    Ram = "8GB DDR4",
                    Drive = "256GB SSD",
                    Other = null,
                    Comment = "Свободный, хранится на складе",
                    Deleted = false,
                    DelDate = null,
                    Employee = null
                },
                new Laptop
                {
                    Id = 4,
                    InventoryNumber = 300000004,
                    Model = "Asus ROG Zephyrus G14",
                    SerialNumber = "SN-ASUS-004",
                    EmployeeId = 4, // Морозов Алексей Николаевич (Логистика)
                    Diagonal = 14.0f,
                    Resolution = "2560x1600",
                    OperatingSystem = "Windows 11 Home",
                    Processor = "AMD Ryzen 9 5900HS",
                    Ram = "32GB DDR4",
                    Drive = "1TB SSD",
                    Other = "NVIDIA RTX 3060",
                    Comment = "Используется для работы с 3D-моделями упаковки",
                    Deleted = false,
                    DelDate = null,
                    Employee = Employees.FirstOrDefault(e => e.Id == 4)
                },
                new Laptop
                {
                    Id = 5,
                    InventoryNumber = 300000005,
                    Model = "Dell Latitude 5490",
                    SerialNumber = "SN-DELL-005",
                    EmployeeId = null,
                    Diagonal = 14.0f,
                    Resolution = "1366x768",
                    OperatingSystem = "Windows 7 Pro",
                    Processor = "Intel Core i3-8130U",
                    Ram = "4GB DDR4",
                    Drive = "500GB HDD",
                    Other = null,
                    Comment = "Устаревшая модель, подлежит списанию",
                    Deleted = true,
                    DelDate = new DateOnly(2024, 9, 30),
                    Employee = null
                }
            }).Object;

        public virtual DbSet<LogEntry> Logs =>
            DbSetMockHelper.CreateMockDbSet(new List<LogEntry>
            {
                new LogEntry
                {
                    Id = 1,
                    ActionDate = DateTime.Now.AddMinutes(-120),
                    UserId = 1,
                    Username = "admin",
                    UserIp = "10.0.0.100",
                    Source = "WebApp",
                    ActionType = "Авторизация",
                    TableName = "UsersProfile",
                    RecordId = null,
                    Changes = null,
                    Specifies = "Успешный вход в систему"
                },
                new LogEntry
                {
                    Id = 2,
                    ActionDate = DateTime.Now.AddMinutes(-90),
                    UserId = 2,
                    Username = "petrov",
                    UserIp = "10.0.0.101",
                    Source = "WebApp",
                    ActionType = "Изменение",
                    TableName = "Orgtechnic",
                    RecordId = 1,
                    Changes = "Изменено поле CartridgeModel: CF259A → CF259X",
                    Specifies = "Обновление информации о картридже принтера"
                },
                new LogEntry
                {
                    Id = 3,
                    ActionDate = DateTime.Now.AddMinutes(-60),
                    UserId = 1,
                    Username = "admin",
                    UserIp = "10.0.0.100",
                    Source = "WebApp",
                    ActionType = "Добавление",
                    TableName = "Monitor",
                    RecordId = 6,
                    Changes = "Добавлен новый монитор Samsung S24F350",
                    Specifies = "Инвентарный номер: 400000006"
                },
                new LogEntry
                {
                    Id = 4,
                    ActionDate = DateTime.Now.AddMinutes(-45),
                    UserId = 3,
                    Username = "sidorova",
                    UserIp = "10.0.0.102",
                    Source = "WebApp",
                    ActionType = "Удаление",
                    TableName = "Tablet",
                    RecordId = 5,
                    Changes = "Планшет Huawei MediaPad T5 перенесён в архив",
                    Specifies = "Причина: Устаревшая модель, неисправен"
                },
                new LogEntry
                {
                    Id = 5,
                    ActionDate = DateTime.Now.AddMinutes(-10),
                    UserId = null,
                    Username = null,
                    UserIp = "192.168.0.150",
                    Source = "WebApp",
                    ActionType = "Неудачная авторизация",
                    TableName = "UsersProfile",
                    RecordId = null,
                    Changes = null,
                    Specifies = "Попытка входа с неверным паролем (логин: guest)"
                }
            }).Object;

        public virtual DbSet<Entities.Monitor> Monitors =>
            DbSetMockHelper.CreateMockDbSet(new List<Entities.Monitor>
            {
                new Entities.Monitor
                {
                    Id = 1,
                    InventoryNumber = 400000001,
                    Model = "Samsung S24F350",
                    SerialNumber = "SN-MON-001",
                    Diagonal = 24.0f,
                    Resolution = "1920x1080",
                    Comment = "Основной монитор для работы",
                    Deleted = false,
                    DelDate = null,
                    VgaPort = 1,
                    DviPort = 0,
                    HdmiPort = 1,
                    DpPort = 0
                },
                new Entities.Monitor
                {
                    Id = 2,
                    InventoryNumber = 400000002,
                    Model = "Dell P2419H",
                    SerialNumber = "SN-MON-002",
                    Diagonal = 24.0f,
                    Resolution = "1920x1080",
                    Comment = "Монитор с возможностью вертикальной установки",
                    Deleted = false,
                    DelDate = null,
                    VgaPort = 0,
                    DviPort = 0,
                    HdmiPort = 1,
                    DpPort = 1
                },
                new Entities.Monitor
                {
                    Id = 3,
                    InventoryNumber = 400000003,
                    Model = "LG UltraFine 27UN83A",
                    SerialNumber = "SN-MON-003",
                    Diagonal = 27.0f,
                    Resolution = "3840x2160",
                    Comment = "Используется для работы с графикой",
                    Deleted = false,
                    DelDate = null,
                    VgaPort = 0,
                    DviPort = 0,
                    HdmiPort = 2,
                    DpPort = 1
                },
                new Entities.Monitor
                {
                    Id = 4,
                    InventoryNumber = 400000004,
                    Model = "Acer V226HQL",
                    SerialNumber = "SN-MON-004",
                    Diagonal = 21.5f,
                    Resolution = "1920x1080",
                    Comment = "Бюджетная модель, отдел кадров",
                    Deleted = false,
                    DelDate = null,
                    VgaPort = 1,
                    DviPort = 1,
                    HdmiPort = 0,
                    DpPort = 0
                },
                new Entities.Monitor
                {
                    Id = 5,
                    InventoryNumber = 400000005,
                    Model = "LG Flatron E2240",
                    SerialNumber = "SN-MON-005",
                    Diagonal = 21.5f,
                    Resolution = "1920x1080",
                    Comment = "Старый монитор, подлежит списанию",
                    Deleted = true,
                    DelDate = new DateOnly(2024, 9, 30),
                    VgaPort = 1,
                    DviPort = 0,
                    HdmiPort = 0,
                    DpPort = 0
                }
            }).Object;

        public virtual DbSet<Orgtechnic> Orgtechnics =>
            DbSetMockHelper.CreateMockDbSet(new List<Orgtechnic>
            {
                new Orgtechnic
                {
                    Id = 1,
                    InventoryNumber = 500000001,
                    Type = "Принтер",
                    Model = "HP LaserJet Pro M404dn",
                    SerialNumber = "SN-PRN-001",
                    Ip = "10.0.1.10",
                    ServiceLogin = "admin",
                    ServicePassword = "password123",
                    CartridgeModel = "CF259A",
                    Workplace = "Кабинет главного бухгалтера",
                    Comment = "Личный принтер Петрова П.П.",
                    Deleted = false,
                    DelDate = null
                },
                new Orgtechnic
                {
                    Id = 2,
                    InventoryNumber = 500000002,
                    Type = "МФУ",
                    Model = "Canon i-SENSYS MF445dw",
                    SerialNumber = "SN-MFU-002",
                    Ip = "10.0.1.11",
                    ServiceLogin = "service",
                    ServicePassword = "canon123",
                    CartridgeModel = "057H",
                    Workplace = "Отдел кадров",
                    Comment = "Используется для сканирования документов",
                    Deleted = false,
                    DelDate = null
                },
                new Orgtechnic
                {
                    Id = 3,
                    InventoryNumber = 500000003,
                    Type = "Сканер",
                    Model = "Epson Perfection V39",
                    SerialNumber = "SN-SCN-003",
                    Ip = null,
                    ServiceLogin = null,
                    ServicePassword = null,
                    CartridgeModel = null,
                    Workplace = "Склад",
                    Comment = "Ручной сканер, не подключён к сети",
                    Deleted = false,
                    DelDate = null
                },
                new Orgtechnic
                {
                    Id = 4,
                    InventoryNumber = 500000004,
                    Type = "Принтер",
                    Model = "Brother HL-L2375DW",
                    SerialNumber = "SN-PRN-004",
                    Ip = "10.0.1.12",
                    ServiceLogin = "admin",
                    ServicePassword = "brother",
                    CartridgeModel = "TN-2421",
                    Workplace = "Логистический отдел",
                    Comment = "Используется для печати накладных",
                    Deleted = false,
                    DelDate = null
                },
                new Orgtechnic
                {
                    Id = 5,
                    InventoryNumber = 500000005,
                    Type = "Принтер",
                    Model = "Samsung ML-2160",
                    SerialNumber = "SN-PRN-005",
                    Ip = null,
                    ServiceLogin = null,
                    ServicePassword = null,
                    CartridgeModel = "MLT-D101S",
                    Workplace = "Архив",
                    Comment = "Старый принтер, подлежит списанию",
                    Deleted = true,
                    DelDate = new DateOnly(2024, 8, 15)
                }
            }).Object;

        public virtual DbSet<Role> Roles =>
            DbSetMockHelper.CreateMockDbSet(new List<Role>
            {
                new Role
                {
                    Id = 1,
                    IsAdministrator = true,
                    EmployeesAccesslevel = RolePermission.ReadWrite,
                    TechnicsAccesslevel = RolePermission.ReadWrite,
                    FurnitureAccesslevel = RolePermission.ReadWrite,
                    RoleName = "Администратор",
                    UsersProfiles = new List<UsersProfile>() // Заполнишь позже при необходимости
                },
                new Role
                {
                    Id = 2,
                    IsAdministrator = false,
                    EmployeesAccesslevel = RolePermission.Read,
                    TechnicsAccesslevel = RolePermission.ReadWrite,
                    FurnitureAccesslevel = RolePermission.Read,
                    RoleName = "Инженер ИТ отдела",
                    UsersProfiles = new List<UsersProfile>()
                },
                new Role
                {
                    Id = 3,
                    IsAdministrator = false,
                    EmployeesAccesslevel = RolePermission.Read,
                    TechnicsAccesslevel = RolePermission.Read,
                    FurnitureAccesslevel = RolePermission.None,
                    RoleName = "Бухгалтер",
                    UsersProfiles = new List<UsersProfile>()
                },
                new Role
                {
                    Id = 4,
                    IsAdministrator = false,
                    EmployeesAccesslevel = RolePermission.Read,
                    TechnicsAccesslevel = RolePermission.Read,
                    FurnitureAccesslevel = RolePermission.Read,
                    RoleName = "Менеджер",
                    UsersProfiles = new List<UsersProfile>()
                },
                new Role
                {
                    Id = 5,
                    IsAdministrator = false,
                    EmployeesAccesslevel = RolePermission.None,
                    TechnicsAccesslevel = RolePermission.None,
                    FurnitureAccesslevel = RolePermission.None,
                    RoleName = "Гость",
                    UsersProfiles = new List<UsersProfile>()
                }
            }).Object;

        public virtual DbSet<RolesView> RolesViews =>
            DbSetMockHelper.CreateMockDbSet(new List<RolesView>
            {
                new RolesView
                {
                    Id = 1,
                    RoleName = "Администратор",
                    EmployeesAccesslevel = "Полный доступ",
                    TechnicsAccesslevel = "Полный доступ",
                    FurnitureAccesslevel = "Полный доступ",
                    IsAdministrator = "Да"
                },
                new RolesView
                {
                    Id = 2,
                    RoleName = "Инженер ИТ отдела",
                    EmployeesAccesslevel = "Чтение",
                    TechnicsAccesslevel = "Полный доступ",
                    FurnitureAccesslevel = "Чтение",
                    IsAdministrator = "Нет"
                },
                new RolesView
                {
                    Id = 3,
                    RoleName = "Бухгалтер",
                    EmployeesAccesslevel = "Чтение",
                    TechnicsAccesslevel = "Чтение",
                    FurnitureAccesslevel = "Нет доступа",
                    IsAdministrator = "Нет"
                },
                new RolesView
                {
                    Id = 4,
                    RoleName = "Менеджер",
                    EmployeesAccesslevel = "Чтение",
                    TechnicsAccesslevel = "Чтение",
                    FurnitureAccesslevel = "Чтение",
                    IsAdministrator = "Нет"
                },
                new RolesView
                {
                    Id = 5,
                    RoleName = "Гость",
                    EmployeesAccesslevel = "Нет доступа",
                    TechnicsAccesslevel = "Нет доступа",
                    FurnitureAccesslevel = "Нет доступа",
                    IsAdministrator = "Нет"
                }
            }).Object;

        public virtual DbSet<Router> Routers =>
            DbSetMockHelper.CreateMockDbSet(new List<Router>
            {
                new Router
                {
                    Id = 1,
                    InventoryNumber = 600000001,
                    Name = "MainRouter",
                    Model = "MikroTik RB4011",
                    Location = "Серверная",
                    Ip = "10.0.0.1",
                    Dhcp = "10.0.0.100 - 10.0.0.200",
                    AdminLogin = "admin",
                    AdminPassword = "securePass123",
                    WifiName = null,
                    WifiPassword = null,
                    Comment = "Основной корпоративный маршрутизатор",
                    Deleted = false,
                    DelDate = null
                },
                new Router
                {
                    Id = 2,
                    InventoryNumber = 600000002,
                    Name = "GuestWiFi",
                    Model = "TP-Link Archer C6",
                    Location = "Ресепшен",
                    Ip = "192.168.0.1",
                    Dhcp = "192.168.0.100 - 192.168.0.200",
                    AdminLogin = "admin",
                    AdminPassword = "tplink123",
                    WifiName = "Guest_Network",
                    WifiPassword = "guest2024",
                    Comment = "Гостевая Wi-Fi сеть для посетителей",
                    Deleted = false,
                    DelDate = null
                },
                new Router
                {
                    Id = 3,
                    InventoryNumber = 600000003,
                    Name = "WarehouseRouter",
                    Model = "Asus RT-AX55",
                    Location = "Склад",
                    Ip = "10.0.2.1",
                    Dhcp = "10.0.2.100 - 10.0.2.200",
                    AdminLogin = "admin",
                    AdminPassword = "warehouse321",
                    WifiName = "WarehouseWiFi",
                    WifiPassword = "stockroom2024",
                    Comment = "Роутер для терминалов на складе",
                    Deleted = false,
                    DelDate = null
                },
                new Router
                {
                    Id = 4,
                    InventoryNumber = 600000004,
                    Name = "OldRouter",
                    Model = "D-Link DIR-615",
                    Location = "Архив",
                    Ip = "192.168.1.1",
                    Dhcp = "192.168.1.50 - 192.168.1.150",
                    AdminLogin = "admin",
                    AdminPassword = "admin",
                    WifiName = "Old_Network",
                    WifiPassword = "oldpassword",
                    Comment = "Устаревший роутер, подлежит списанию",
                    Deleted = true,
                    DelDate = DateTime.Parse("2024-09-01")
                }
            }).Object;

        public virtual DbSet<Entities.Switch> Switches =>
            DbSetMockHelper.CreateMockDbSet(new List<Entities.Switch>
            {
                new Entities.Switch
                {
                    Id = 1,
                    InventoryNumber = 700000001,
                    Name = "CoreSwitch",
                    Model = "Cisco Catalyst 2960-X",
                    Location = "Серверная",
                    Ip = "10.0.0.2",
                    Dhcp = null,
                    AdminLogin = "admin",
                    AdminPassword = "ciscoAdmin2024",
                    Comment = "Основной L2 коммутатор",
                    Deleted = false,
                    DelDate = null
                },
                new Entities.Switch
                {
                    Id = 2,
                    InventoryNumber = 700000002,
                    Name = "OfficeSwitch1",
                    Model = "TP-Link TL-SG1024DE",
                    Location = "Офис, этаж 1",
                    Ip = "10.0.1.2",
                    Dhcp = null,
                    AdminLogin = "admin",
                    AdminPassword = "tplinkSwitch",
                    Comment = "Управляемый свитч в офисе, первый этаж",
                    Deleted = false,
                    DelDate = null
                },
                new Entities.Switch
                {
                    Id = 3,
                    InventoryNumber = 700000003,
                    Name = "WarehouseSwitch",
                    Model = "D-Link DGS-1100-24",
                    Location = "Склад",
                    Ip = "10.0.2.2",
                    Dhcp = null,
                    AdminLogin = "admin",
                    AdminPassword = "dlinkPass",
                    Comment = "Складской свитч для терминалов и камер",
                    Deleted = false,
                    DelDate = null
                },
                new Entities.Switch
                {
                    Id = 4,
                    InventoryNumber = 700000004,
                    Name = "OldSwitch",
                    Model = "Cisco Catalyst 2960",
                    Location = "Архив",
                    Ip = "192.168.1.2",
                    Dhcp = null,
                    AdminLogin = "admin",
                    AdminPassword = "admin",
                    Comment = "Старый свитч, подлежит списанию",
                    Deleted = true,
                    DelDate = DateTime.Parse("2024-08-20")
                }
            }).Object;

        public virtual DbSet<Tablet> Tablets =>
            DbSetMockHelper.CreateMockDbSet(new List<Tablet>
            {
                new Tablet
                {
                    Id = 1,
                    InventoryNumber = 800000001,
                    Model = "Apple iPad Pro 11\" (2022)",
                    SerialNumber = "SN-IPAD-001",
                    EmployeeId = 6, // Зайцева Мария Владимировна (Маркетинг)
                    OperatingSystem = "iPadOS 17",
                    Diagonal = 11.0f,
                    Processor = "Apple M2",
                    Ram = "8GB",
                    Drive = "256GB SSD",
                    Other = "Wi-Fi + LTE, Face ID, Apple Pencil 2nd Gen",
                    Comment = "Используется для презентаций",
                    Deleted = false,
                    DelDate = null
                },
                new Tablet
                {
                    Id = 2,
                    InventoryNumber = 800000002,
                    Model = "Samsung Galaxy Tab S9",
                    SerialNumber = "SN-TAB-002",
                    EmployeeId = 4, // Морозов Алексей Николаевич (Логистика)
                    OperatingSystem = "Android 14",
                    Diagonal = 11.0f,
                    Processor = "Snapdragon 8 Gen 2",
                    Ram = "12GB",
                    Drive = "512GB",
                    Other = "Wi-Fi + 5G, S-Pen",
                    Comment = "Используется для учёта на складе",
                    Deleted = false,
                    DelDate = null
                },
                new Tablet
                {
                    Id = 3,
                    InventoryNumber = 800000003,
                    Model = "Lenovo Tab M10",
                    SerialNumber = "SN-TAB-003",
                    EmployeeId = null, // Не выдан
                    OperatingSystem = "Android 12",
                    Diagonal = 10.1f,
                    Processor = "MediaTek Helio P22T",
                    Ram = "4GB",
                    Drive = "64GB",
                    Other = null,
                    Comment = "Свободный, хранится на складе",
                    Deleted = false,
                    DelDate = null
                },
                new Tablet
                {
                    Id = 4,
                    InventoryNumber = 800000004,
                    Model = "Microsoft Surface Pro 7",
                    SerialNumber = "SN-SURFACE-004",
                    EmployeeId = 1, // Иванов Иван Иванович (ИТ)
                    OperatingSystem = "Windows 11 Pro",
                    Diagonal = 12.3f,
                    Processor = "Intel Core i5-1035G4",
                    Ram = "8GB",
                    Drive = "256GB SSD",
                    Other = "Type Cover, Surface Pen",
                    Comment = "Для удалённой работы",
                    Deleted = false,
                    DelDate = null
                },
                new Tablet
                {
                    Id = 5,
                    InventoryNumber = 800000005,
                    Model = "Huawei MediaPad T5",
                    SerialNumber = "SN-TAB-005",
                    EmployeeId = null,
                    OperatingSystem = "Android 10",
                    Diagonal = 10.1f,
                    Processor = "Kirin 659",
                    Ram = "3GB",
                    Drive = "32GB",
                    Other = null,
                    Comment = "Устаревшая модель, подлежит списанию",
                    Deleted = true,
                    DelDate = new DateOnly(2024, 7, 15)
                }
            }).Object;

        public virtual DbSet<UserProfileView> UserProfileViews =>
            DbSetMockHelper.CreateMockDbSet(new List<UserProfileView>
            {
                new UserProfileView
                {
                    Id = 1,
                    Username = "admin",
                    Fio = "Иванов Иван Иванович",
                    PasswordStatus = "Есть",
                    RoleName = "Администратор"
                },
                new UserProfileView
                {
                    Id = 2,
                    Username = "petrov",
                    Fio = "Петров Пётр Петрович",
                    PasswordStatus = "Сброшен",
                    RoleName = "Бухгалтер"
                },
                new UserProfileView
                {
                    Id = 3,
                    Username = "sidorova",
                    Fio = "Сидорова Анна Сергеевна",
                    PasswordStatus = "Сброшен",
                    RoleName = "Менеджер"
                },
                new UserProfileView
                {
                    Id = 4,
                    Username = "guest",
                    Fio = null,
                    PasswordStatus = "Есть",
                    RoleName = "Гость"
                },
                new UserProfileView
                {
                    Id = 5,
                    Username = "egorov",
                    Fio = "Егоров Дмитрий Валерьевич",
                    PasswordStatus = "Есть",
                    RoleName = "Инженер ИТ отдела"
                }
            }).Object;

        public virtual DbSet<UsersProfile> UsersProfiles =>
            DbSetMockHelper.CreateMockDbSet(new List<UsersProfile>
            {
                new UsersProfile
                {
                    Id = 1,
                    Username = "admin",
                    UserPassword = "8c6976e5b5410415bde908bd4dee15dfb16b4eaa68b8f8e8e5a67e6bafad6c3e", // "admin123"
                    Role = 1,
                    EmployeeId = 1,
                    RoleNavigation = Roles.First(r => r.Id == 1)
                },
                new UsersProfile
                {
                    Id = 2,
                    Username = "petrov",
                    UserPassword = null, // Пароль сброшен, пользователь должен установить новый
                    Role = 3,
                    EmployeeId = 2,
                    RoleNavigation = Roles.First(r => r.Id == 3)
                },
                new UsersProfile
                {
                    Id = 3,
                    Username = "sidorova",
                    UserPassword = "", // Тоже сброшен
                    Role = 4,
                    EmployeeId = 3,
                    RoleNavigation = Roles.First(r => r.Id == 4)
                },
                new UsersProfile
                {
                    Id = 4,
                    Username = "guest",
                    UserPassword = "084e0343a0486ff05530df6c705c8bb4e472b6c7cde3e7e8561a69f8b5b9e3ab", // "guest"
                    Role = 5,
                    EmployeeId = null,
                    RoleNavigation = Roles.First(r => r.Id == 5)
                },
                new UsersProfile
                {
                    Id = 5,
                    Username = "egorov",
                    UserPassword = "c9c5a05cb8a5dc6a850d0a9eaa3e9cf763fd0d20ccabcc7e23d6b5b7fc7d8f44", // "itEngineer2024"
                    Role = 2,
                    EmployeeId = 5,
                    RoleNavigation = Roles.First(r => r.Id == 2)
                }
            }).Object;

        public override int SaveChanges() => 0;
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(0);

        // Assistant methods
        private static long IpToLong(string ip)
        {
            return ip.Split('.').Select(byte.Parse).Aggregate(0L, (acc, b) => (acc << 8) + b);
        }
    }
}
