# AcademyProject

Платформата има за цел да предостави възможност на потребителите за разглеждат курсове, които могат да започнат да изучават. Всеки регистриран потребител има свой собствен акаунт, който има определена роля и възможности. Логнатите потребители могат да пишат постове и да коментират постове на други потребители. <br />
Основните роли са: <br />

 * User -> Всеки потребител, който има регистрация получава тази роля, чрез тази роля потребителите могат да пишат постове, да коментират постове, да разглеждат налични курсове.
 * Student -> Има същите правила като User, но има възможност да записва курсове. Има достъп до всички курсове, които е записал. 
 * Administrator -> Може да добавя, редактира и изтрива нови курсове, учители и ученици. Има право да трие коментари и постове.

  ## Models <br />
  * BaseModel -> Базов клас, който се наследевя от всички класове
     - Id <br />
  * User <br />
     - Id
     - Email 
     - Profession 
     - Country 
     - ImageName 
     - ImageFile 
     
  * Teacher <br />
     - Id
     - FirstName
     - SecondName
     - LastName
     - Education
     - Year
     - Experience 
     - Position
     - Salary
     - PhoneNumber
     - Email
     - ImageName 
     - ImageFile 
     
  * Student <br />
     - Id
     - FirstName
     - SecondName
     - LastName
     - Year
     - City
     - PhoneNumber
     - Email
     - ImageName
     - ImageFile
     
  * Course <br />
     - Id
     - Name
     - Description 
     - Duration
     - Price
     - ImageName
     - ImageFile
     
  * Post <br />
     - Id
     - Title
     - Content
     
  * Comment <br />
     - Id
     - Content
  <br />
   
  ## Services
 *  BaseService -> Наследява се от всеки service
  ### Implementation 
 *  CourseService -> Основните CRUD операции, свързани със Course модела
 *  PostService -> Основните CRUD операции, свързани с Post модела
 *  StudentService -> Основните CRUD операции, свързани с Student модела
 *  TeacherService -> Основните CRUD операции, свързани с Teacher модела
 *  UserService -> Основните CRUD операции, свързани с User модела
  
  ### Interfaces
 *  ICourseService, IManagerService, IPostService, IStudentService, ITeacherService, 
IUserService 
 
  ### ViewModels -> Модели, чрез които потребителите правят промени по главните модели:
 * CommentViewModel.cs, CourseViewMode, ManagerViewModel, PostViewModeL, StudentViewModel, 
TeacherViewModel, UserViewModel

  ## Controllers <br />
 * HomeController
 * CourseController
 * DashboardController
 * PostController
 * StudentController
 * TeacherController
 * UserController

   ## Views<br />
