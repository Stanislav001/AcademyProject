# AcademyProject

Платформата има за цел да предостави възможност на потребителите за разглеждат курсове, които могат да започнат да изучават. Всеки регистриран потребител има свой собствен акаунт, който има определена роля и възможности. Логнатите потребители могат да пишат постове и да коментират постове на други потребители. Могат да дават своя глас, когато даден курс им е харесал. Съответно всеки потребител може да види кои са най-харесваните курсове. Всеки потребител може да зададе даден курс като "Започнат", така може да вижда всички, курсове, които е започнал. <br />
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
 *  CoursesUserService -> Логика за възможността на потребителите да дават своя глас за даден курс
 *  TopCoursesService -> Връща топ 10 курсовете, които са оценени най-добре
 *  PostService -> Основните CRUD операции, свързани с Post модела
 *  UserService -> Основните CRUD операции, свързани с User модела
 *  SaveCourseUserService - > Съдържа логиката, която дава възможност на потребителите да записват даден курс
  
  ### Interfaces
 *  ICourseService, IManagerService, IPostService, 
    IUserService, ITopCoursesService, ISaveCourseUserService
 
  ### ViewModels -> Модели, чрез които потребителите правят промени по главните модели:
 * CommentViewModel.cs, CourseViewMode, ManagerViewModel, PostViewModeL, UserViewModel, TopCourseViewModel, GetAllTopCoursesViewModel, SaveCourseUser

  ## Controllers <br />
 * HomeController
 * CourseController
 * DashboardController
 * PostController
 * UserController
 * TopCoursesController

   ## Views<br />
