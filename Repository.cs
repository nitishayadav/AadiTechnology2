using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginTask.Models;

namespace LoginTask.Models
{
    public class Repository
    {

        public UserModel CheckValid(string username, string password)
        {
            using (var context = new CompanyEntities())
            {
                //var result = context.User.Where(x => x.UserName == username && x.Password == password).
                //    Select(x => new UserModel
                //    {
                //        Id = x.Id,
                //        UserName = x.UserName,
                //        Password = x.Password,
                //        UserRole1 = new UserRole1Model
                //        {
                //           Id=x.UserRole1.FirstOrDefault().Id,
                //           UserId=x.UserRole1.FirstOrDefault().UserId,
                //           Role=x.UserRole1.FirstOrDefault().Role
                //        }
                //    }).FirstOrDefault();

                var userRecord = (from u in context.User
                                 join r in context.UserRole1 on u.Id equals r.UserId
                                 where u.UserName==username && u.Password==password
                                 select new UserModel
                                 {
                                    Id=u.Id,
                                    UserName=u.UserName,
                                    Password=u.Password,
                                   UserRole1=new UserRole1Model
                                   {
                                       Id=r.Id,
                                       UserId=r.UserId,
                                       Role=r.Role
                                   }

                                 }).FirstOrDefault();
                return userRecord;



            }
        }

        public ProfileModel GetUserProfile(int Userid)
        {
            using (var context = new CompanyEntities())
            {
                var result = context.User.Where(x => x.Id == Userid).Select(x => new ProfileModel
                {
                    id = x.Id,
                    UserName = x.UserName,
                    Role = new UserRole1Model()
                    {
                        Role = x.UserRole1.FirstOrDefault().Role
                    }

                }).FirstOrDefault();

                return result;
            }


        }

        public List<CourseModel> CourseDetails()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Course
                    .Select(x => new CourseModel
                    {
                        Id = x.Id,
                        CourseName = x.CourseName
                    }).ToList();
                return result;
            }
        }

        public int AddCourse(CourseModel model)
        {
            using (var context = new CompanyEntities())
            {
                Course course = new Course()
                {
                    Id = model.Id,
                    CourseName = model.CourseName,

                };
                context.Course.Add(course);
                context.SaveChanges();

                return course.Id;
            }
        }

        public CourseModel GetCourseName(int id)
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Course.Where(x => x.Id == id)
                    .Select(x => new CourseModel()
                    {
                        Id = x.Id,
                        CourseName = x.CourseName,

                    }).FirstOrDefault();
                return result;

            }
        }

        public bool UpdateCourse(int id, CourseModel model)
        {
            using (var context = new CompanyEntities())
            {
                //======entity state code=========

                var course = new Course();
                course.Id = model.Id;
                course.CourseName = model.CourseName;

                context.Entry(course).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }

        }
        public bool DeleteCourse(int id)
        {
            using (var context = new CompanyEntities())
            {
                //===========Entity State Code========
                var course = new Course()
                {
                    Id = id
                };
                context.Entry(course).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return false;


            }
        }

        public List<CourseModel> GetCourseName()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Course
                    .Select(x => new CourseModel()
                    {
                        Id = x.Id,
                        CourseName = x.CourseName,
                        Topic = x.Topic.Select(y => new TopicModel()
                        {
                            TopicId = y.TopicId,
                            TopicName = y.TopicName
                        }).ToList()
                    }).ToList();
                return result;
            }
        }

        public int AddTopic(TopicModel model)
        {
            using (var context = new CompanyEntities())
            {
                Topic topic = new Topic()
                {
                    //TopicId = model.TopicId,
                    TopicName = model.TopicName,
                    CourseId = model.CourseId

                };
                context.Topic.Add(topic);
                context.SaveChanges();

                return topic.TopicId;
            }
        }

        public List<TopicModel> TopicDetailsList()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Topic
                    .Select(x => new TopicModel
                    {
                        TopicId = x.TopicId,
                        TopicName = x.TopicName,
                        Course = new CourseModel()
                        {
                            CourseName = x.Course.CourseName
                        }
                    }).ToList();
                return result;
            }
        }

        public TopicModel GetTopicName(int id)
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Topic.Where(x => x.TopicId == id)
                    .Select(x => new TopicModel()
                    {
                        TopicId = x.TopicId,
                        TopicName = x.TopicName,
                        CourseId = x.CourseId

                    }).FirstOrDefault();
                return result;

            }

        }

        public bool UpdateTopic(int id, TopicModel model)
        {
            using (var context = new CompanyEntities())
            {
                //======entity state code=========

                var topic = new Topic();
                topic.TopicId = model.TopicId;
                topic.TopicName = model.TopicName;
                topic.CourseId = model.CourseId;


                context.Entry(topic).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }

        }

        public bool DeleteTopicName(int id)
        {
            using (var context = new CompanyEntities())
            {
                //===========Entity State Code========
                var topic = new Topic()
                {
                    TopicId = id

                };
                context.Entry(topic).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
              
                return false;


            }
        }

        public List<TopicModel> GetTopicName()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Topic
                    .Select(x => new TopicModel()
                    {
                        TopicId = x.TopicId,
                        TopicName = x.TopicName,
                    }).ToList();
                return result;
            }
        }
        public int AddSubTopic(SubTopicModel model)
        {
            using (var context = new CompanyEntities())
            {
                SubTopic subtopic = new SubTopic()
                {
                    //Id = model.Id,
                    SubTopicName = model.SubTopicName,
                    TopicId = model.TopicId,
                    Id=model.Topic.CourseId
                    

                };
                context.SubTopic.Add(subtopic);
                context.SaveChanges();

                return subtopic.Id;
            }
        }

        public List<SubTopicModel> SubTopicDetailsList()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.SubTopic
                    .Select(x => new SubTopicModel
                    {
                        Id = x.Id,
                        SubTopicName = x.SubTopicName,
                        Topic = new TopicModel()
                        {
                            TopicName = x.Topic.TopicName
                        }
                    }).ToList();
                return result;
            }
        }

        public bool DeleteSubTopicName(int id)
        {
            using (var context = new CompanyEntities())
            {
                //===========Entity State Code========
                var subtopic = new SubTopic()
                {
                    Id = id
                };
                context.Entry(subtopic).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return false;


            }
        }

        public List<SubTopicModel> GetSubTopicName()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.SubTopic
                    .Select(x => new SubTopicModel()
                    {
                        Id = x.Id,
                        SubTopicName = x.SubTopicName,
                    }).ToList();
                return result;
            }
        }


        public int AddTask(DailyTask1Model model)
        {

            using (var context = new CompanyEntities())
            {
                DailyTask1 dailyTask = new DailyTask1()
                {
                    Id = model.Id,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    EstimatedHr = model.EstimatedHr,
                    SubTopicId = model.Id,
                    //CourseId = model.SubTopic.Topic.Course.CourseId,
                    //TopicId = model.SubTopic.Topic.TopicId
                };
                context.DailyTask1.Add(dailyTask);
                context.SaveChanges();
                return dailyTask.Id;
            }


        }

        public SubTopicModel GetSubTopicName(int id)
        {
            using (var context = new CompanyEntities())
            {
                var result = context.SubTopic.Where(x => x.Id == id)
                    .Select(x => new SubTopicModel()
                    {
                        Id = x.Id,
                        SubTopicName = x.SubTopicName,
                        TopicId = x.TopicId

                    }).FirstOrDefault();
                return result;

            }

        }

        public bool UpdateSubTopic(int id, SubTopicModel model)
        {
            using (var context = new CompanyEntities())
            {
                //======entity state code=========

                var Subtopic = new SubTopic();
                Subtopic.Id = model.Id;
                Subtopic.SubTopicName = model.SubTopicName;
                Subtopic.TopicId = model.TopicId;


                context.Entry(Subtopic).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        public DailyTask1Model GetTaskName(int id)
        {
            using (var context = new CompanyEntities())
            {
                var result = context.DailyTask1.Where(x => x.Id == id)
                    .Select(x => new DailyTask1Model()
                    {
                        Id = x.Id,
                        //CourseId=x.CourseId,
                        SubTopicId = x.SubTopicId,
                        //TopicId = x.TopicId,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        EstimatedHr = x.EstimatedHr

                    }).FirstOrDefault();
                return result;

            }

        }

        public bool UpdateTask(int id, DailyTask1Model model)
        {
            using (var context = new CompanyEntities())
            {
                //======entity state code=========

                var task = new DailyTask1();
                task.Id = model.Id;
                task.SubTopicId = model.SubTopicId;
                task.StartTime = model.StartTime;
                task.EndTime = model.EndTime;
                task.EstimatedHr = model.EstimatedHr;

                context.Entry(task).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        public List<DailyTask1Model> TaskDetailsList()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.DailyTask1
                    .Select(x => new DailyTask1Model()
                    {
                        Id = x.Id,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        EstimatedHr = x.EstimatedHr,
                        SubTopic = new SubTopicModel()
                        {
                            SubTopicName=x.SubTopic.SubTopicName,
                            Topic=new TopicModel()
                            {
                                TopicName=x.SubTopic.Topic.TopicName,
                                Course=new CourseModel()
                                {
                                    CourseName=x.SubTopic.Topic.Course.CourseName
                                }
                            }
                            
                        }
                       

                    }).ToList();
                return result;
            }
        }

        public List<TopicModel> GetTopicListByCourseId(int CourseId)
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Topic.Where(x => x.CourseId == CourseId)
                    .Select(x => new TopicModel()
                    {
                        TopicId=x.TopicId,
                        TopicName = x.TopicName

                    }).ToList();
                return result;
            }
        }

        public List<SubTopicModel> GetSubTopicListByTopicId(int TopicId)
        {
            using (var context = new CompanyEntities())
            {
                var result = context.SubTopic.Where(x => x.TopicId == TopicId)
                    .Select(x => new SubTopicModel()
                    {
                       Id=x.Id,
                       SubTopicName=x.SubTopicName

                    }).ToList();
                return result;
            }
        }

        public List<CustomerModel> CustomerDetails()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Customer
                    .Select(x => new CustomerModel
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Contact = x.Contact,
                        Address = x.Address
                    }).ToList();
                return result;
            }
        }

        public List<Manufacturer1Model> manufacturerDetails()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Manufacturer1
                    .Select(x => new Manufacturer1Model
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Contact = x.Contact,
                        Address = x.Address
                    }).ToList();
                return result;
            }
        }

        public List<OwnerModel> ownerDetails()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Owner
                    .Select(x => new OwnerModel
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Contact = x.Contact,
                        Address = x.Address
                    }).ToList();
                return result;
            }
        }
      public List<CourseModel> CourseChartQuery()
        {
            using (var context = new CompanyEntities())
            {
                var result = context.Course
                              .GroupBy(x =>x.CourseName)
                                .Select(y => new CourseModel
                         {
                                CourseName = y.First().CourseName,
                                count = y.Count()
                         }).ToList();
                return result;
            }
        }

    }
}