// Інтерфейс для користувача (тільки властивості)
interface IUser {
    id: number;
    name: string;
    role: string;
    age: number;
    isActive: boolean;
  }
  
  // Клас User для роботи з користувачами (реалізація методів)
  class User implements IUser {
    constructor(
      public id: number,
      public name: string,
      public role: string,
      public age: number,
      public isActive: boolean
    ) {}
  
    // Метод для зміни активності користувача
    toggleActive(): void {
      this.isActive = !this.isActive;
    }
  
    // Метод для перевірки прав доступу
    checkAccess(): string {
      if (this.role === 'admin') {
        return 'Admin Access';
      } else if (this.role === 'member') {
        return 'Member Access';
      } else {
        return 'Guest Access';
      }
    }
  }
  
  // Батьківський клас для створення групи користувачів
  class UserGroup {
    users: IUser[];
  
    constructor(users: IUser[]) {
      this.users = users;
    }
  
    // Метод для додавання нового користувача
    addUser(user: IUser): void {
      this.users.push(user);
    }
  
    // Метод для перевірки доступу всіх користувачів
    checkAllAccess(): void {
      for (let i = 0; i < this.users.length; i++) {
        const user = this.users[i];
        if (user instanceof User) {
          console.log(`${user.name}: ${user.checkAccess()}`);
        }
      }
    }
  
    // Метод для фільтрації активних користувачів
    filterActiveUsers(): IUser[] {
      return this.users.filter(user => user.isActive);
    }
  }
  
  // Підклас для групи адміністраторів
  class AdminGroup extends UserGroup {
    // Метод для отримання лише адміністраторів
    getAdmins(): IUser[] {
      return this.users.filter(user => user.role === 'admin');
    }
  }
  
  // Створення об'єктів
  const users: IUser[] = [
    new User(1, 'Alice', 'admin', 30, true),
    new User(2, 'Bob', 'member', 25, false),
    new User(3, 'Charlie', 'guest', 22, true),
  ];
  
  // Створення групи користувачів
  const group = new UserGroup(users);
  
  // Перевірка доступу для всіх користувачів
  group.checkAllAccess();
  
  // Використовуємо метод filterActiveUsers для отримання тільки активних користувачів
  const activeUsers = group.filterActiveUsers();
  console.log('Active users:');
  activeUsers.forEach(user => console.log(user.name));
  
  // Створюємо групу адміністраторів та отримуємо адміністраторів
  const adminGroup = new AdminGroup(users);
  const admins = adminGroup.getAdmins();
  console.log('Admins:');
  admins.forEach(admin => console.log(admin.name));
  
  // Демонстрація використання switch-case для визначення віку користувача
  function getUserCategory(user: IUser): string {
    switch (true) {
      case user.age < 18:
        return 'Underage';
      case user.age >= 18 && user.age < 30:
        return 'Young Adult';
      case user.age >= 30 && user.age < 50:
        return 'Adult';
      default:
        return 'Senior';
    }
  }
  
  // Визначаємо категорії для кожного користувача
  users.forEach(user => {
    const category = getUserCategory(user);
    console.log(`${user.name} is categorized as: ${category}`);
  });
  
  // Використовуємо цикли for і for...of для ітерації
  console.log('Users Info:');
  for (let i = 0; i < users.length; i++) {
    const user = users[i];
    console.log(`${user.name}, Role: ${user.role}, Age: ${user.age}`);
  }
  
  // Цикл for...of для виведення лише активних користувачів
  console.log('Active Users Info:');
  for (const user of activeUsers) {
    console.log(`${user.name} is active.`);
  }
  
  // Розділ для перевірки прав доступу
  users.forEach(user => {
    if (user instanceof User && user.isActive) {
      console.log(`${user.name} has access to the platform.`);
    } else {
      console.log(`${user.name} does not have access due to inactivity.`);
    }
  });
  
