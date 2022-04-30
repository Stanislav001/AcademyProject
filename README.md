# AcademyProject

The platform aims to provide users with the opportunity to view courses that they can start studying. Each registered user has his own account, which has a certain function and capability. Registered users can write posts and comment on other users' posts. They can cast their vote when they like a course. Accordingly, each user can see which are the most popular courses. Each user can give a course as "Started", so you can see all the courses that have started.<br />

The main roles are: <br />

 * User -> Every user who has a registration gets this role, through this role users can write posts, comment on posts, view available courses.
 * Student -> It has the same rules as User, but has the ability to enroll in courses. He has access to all the courses he has enrolled in.
 * Administrator -> Can add, edit and delete new courses, teachers and students. He has the right to delete comments and posts.

  ## Models <br />
  * BaseModel -> A base class that is inherited from all classes
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
 *  BaseService -> Inherited from each service
  ### Implementation 
 *  CourseService -> The main CRUD operations related to the Course model
 *  CoursesUserService -> Logic for the ability of users to vote for a course
 *  TopCoursesService -> Returns the top 10 courses that are rated best
 *  PostService -> The main CRUD operations related to the Post model
 *  UserService -> The main CRUD operations related to the User model
 *  SaveCourseUserService - > It contains the logic that allows users to enroll in a course
  
  ### Interfaces
 *  ICourseService, IManagerService, IPostService, 
    IUserService, ITopCoursesService, ISaveCourseUserService
 
  ### ViewModels -> Models through which users make changes to the main models:
 * CommentViewModel.cs, CourseViewMode, ManagerViewModel, PostViewModeL, UserViewModel, TopCourseViewModel, GetAllTopCoursesViewModel, SaveCourseUser

  ## Controllers <br />
 * HomeController
 * CourseController
 * DashboardController
 * PostController
 * UserController
 * TopCoursesController
