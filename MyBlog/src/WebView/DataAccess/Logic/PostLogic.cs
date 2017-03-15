using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebBlog.DataAccess.Other;
using WebBlog.DataAccess.Model;
using WebBlog.DataAccess;
using WebBlog.DataAccess.ViewModel;

namespace WebBlog.Model
{

    public class PostLogic
    {
        int maxGetPosts;

        public PostLogic(int maxGetPosts)
        {
            this.maxGetPosts = maxGetPosts;
        }

        public Tuple<int, List<PostView>> GetPostCollection(List<SortFiltr> filters = null, int page = 1)
        {

            //фильтры

            int _count = 0;
            List<PostView> posts=new List<PostView>();


            string filterrequesr=String.Empty;

            foreach (var filter in filters)
                switch (filter.SortType)
                {
                    case SortType.DateINC:
                        filterrequesr = "[Date] ASC"; break;
                    case SortType.DateDEC:
                        filterrequesr = "[Date] DESC"; break;
                    case SortType.RetingBest:
                        filterrequesr = "[Rating].[Like] ASC"; break;
                    case SortType.RetingBad:
                        filterrequesr = "[Rating].[Dislike] DESC"; break;
                    case SortType.Popular:
                        filterrequesr = "[Views] ASC"; break;
                }

            try
            {
                using (DatabaseHelper dbConnect = new DatabaseHelper(false))
                {
                    Parallel.Invoke(
                         () =>
                         {
                             _count = dbConnect.Query<int>($@"SELECT COUNT(*) FROM [blogappdb].[dbo].[Post] ORDER BY {filterrequesr}", null).First();
                         },
                         () =>
                         {
                             string mainQuery = $@"SELECT {DatabaseHelper.GetFilds(typeof(PostView))} FROM 
                        (SELECT * FROM [blogappdb].[dbo].[Post] WHERE [StatePost]=0) AS [Post] 
                        LEFT JOIN (SELECT Id, Name as [NameCategory], ImagePath as [ImagePathCategory] FROM [blogappdb].[dbo].[Category]) AS [Category] ON [Post].[Id] = [Category].[Id]
                        LEFT JOIN (SELECT Id, UserName as [UserName], ImagePath as [ImagePath] FROM [blogappdb].[dbo].[User]) AS [User] ON [Post].[Id] = [User].[Id]
                        LEFT JOIN (SELECT Id, Like as [Like], Dislike as [Dislike] FROM [blogappdb].[dbo].[Rating]) AS [Rating] ON [Post].[Id] = [Rating].[Id]
                        ORDER BY {filterrequesr} OFFSET({ (page - 1) * maxGetPosts}) ROWS FETCH FIRST { maxGetPosts}  ROWS ONLY";


                             posts = dbConnect.Query<PostView>(mainQuery, parameters: null).ToList();

                         });

                    Parallel.ForEach(posts, (post) =>
                    {
                        post.Tags = dbConnect.Query<Tag>($"SELECT * FROM [blogappdb].[dbo].[Tag] WHERE [Id] IN (SELECT [IdTag] FROM [blogappdb].[dbo].[TagCollection] WHERE [Post_Id]= @post.Id)", new { post.Id }).ToList();
                    });
                       
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return new Tuple<int, List<PostView>>(_count, posts);
        }



        public Post GetPost(Guid IdPost)
        {
            Post post = null;
            List<Tag> tags = null;
            List<Coment> coments = null;
            try
            {
                using (DatabaseHelper dbConnect = new DatabaseHelper(false))
                {


                    Parallel.Invoke(
                            () =>
                            {
                                string query = $@"SELECT * FROM 
                        (SELECT * FROM [blogappdb].[dbo].[Post] WHERE [StatePost]=0) AS [Post] 
                        LEFT JOIN (SELECT * FROM [blogappdb].[dbo].[Category]) AS [Category] ON [Post].[Id] = [Category].[Id]
                        LEFT JOIN (SELECT * FROM [blogappdb].[dbo].[User]) AS [User] ON [Post].[Id] = [User].[Id]
                        LEFT JOIN (SELECT * FROM [blogappdb].[dbo].[Rating]) AS [Rating] ON [Post].[Id] = [Rating].[Id]";

                                post = dbConnect.Query<Post>("", null).FirstOrDefault();
                            },
                                () =>
                             {
                                 tags = dbConnect.Query<Tag>($"SELECT * FROM [blogappdb].[dbo].[Tag] WHERE [Id] IN (SELECT [IdTag] FROM [blogappdb].[dbo].[TagCollection] WHERE [Post_Id]= @IdPost)", new { IdPost }).ToList();

                             },
                                () =>
                             {
                                 coments = dbConnect.Query<Coment>($"SELECT * FROM [blogappdb].[dbo].[Coment] WHERE [Id] IN (SELECT [IdTag] FROM [blogappdb].[dbo].[TagCollection] WHERE [Post_Id]= @IdPost)", new { IdPost }).ToList();
                             });
                    post.Coments = coments;
                    post.Tags = tags;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return post;
        }




        }

    }



   

