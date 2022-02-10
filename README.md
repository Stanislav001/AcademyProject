# AcademyProject

  ## Models <br />
  * BaseModel -> Базов клас, който се наследевя от всички класове <br />
  * User  <br />
  * Manager <br />
  * Teacher <br />
  * Student <br />
  * Course <br />
  * Grade <br />
  * Post <br />
  * Comment <br />
  <br />
   
  ## Services
 *  BaseService -> Наследява се от всеки service
  ### Implementation 
 *  CourseService -> Основните CRUD операции, свързани със Course модела
 *  GradeService -> Основните CRUD операции, свързани с Grade модела
 *  ManagerService -> Основните CRUD операции, свързани с Manager модела
 *  PostService -> Основните CRUD операции, свързани с Post модела
 *  StudentService -> Основните CRUD операции, свързани с Student модела
 *  TeacherService -> Основните CRUD операции, свързани с Teacher модела
 *  UserService -> Основните CRUD операции, свързани с User модела
  
  ### Interfaces
 *  ICourseService, IGradeService, IManagerService, IPostService, IStudentService, ITeacherService, 
IUserService 
 
  ### ViewModels
 * CommentViewModel.cs, CourseViewMode, GradeViewModel, ManagerViewModel, PostViewModeL, StudentViewModel, 
TeacherViewModel, UserViewModel

  ## Controllers <br />
 * HomeController
 * CourseController
 * DashboardController
 * GradeController
 * ManagerController
 * PostController
 * StudentController
 * TeacherController
 * UserController

   ## Views<br />
