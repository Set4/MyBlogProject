
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelCore
{
    public interface IComentsLogic
    {
        List<Coment> GetComentCollection(int postId);

        Coment GetComent(int id);
        Task<int> CreatePost(int PostId, string UserId, string Text, int? ComentId = null);
        Task<int> ChangePost(Coment coment);
    }

    public class ComentsLogic : IComentsLogic
    {
        DataBaseContext db;      

        public ComentsLogic(DataBaseContext dbContext)
        {
            db = dbContext;           
        }

        public async Task<int> ChangePost(Coment coment)
        {
            coment.DateChange = DateTime.Now;
            db.Coments.Update(coment);
            return await db.SaveChangesAsync();
        }

        public async Task<int> CreatePost(int PostId, string UserId, string Text, int? ComentId = null)
        {
            Coment coment = new Coment(PostId, UserId, Text, ComentId);
            db.Coments.Add(coment);
            await db.SaveChangesAsync();
            return coment.Id;
        }

        public List<Coment> GetComentCollection(int postId)
        {
            List<Coment> coments = db.Coments.Where(c => c.PostId == postId).ToList();
            return coments;
        }

        public Coment GetComent(int id)
        {
            Coment coment = db.Coments.FirstOrDefault(c => c.Id== id);
            return coment;
        }
    }
}
