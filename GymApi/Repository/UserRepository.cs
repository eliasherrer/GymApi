//using GymApi.Context;
//using GymApi.Models;
//using Microsoft.EntityFrameworkCore;

//namespace GymApi.Repository
//{
//    public class UserRepository : IRepository<User>
//    {
//        private GymContext _context;


//        public UserRepository(GymContext context)
//        {
//            _context = context;
//        }

//        public  async Task<IEnumerable<User>> Get() 
//            => await _context.Users.ToListAsync();


//        public async Task<User> GetById(int id)
//        => await _context.Users.FindAsync(id);


//        public async Task Create(User entity) 
//            => await _context.Users.AddAsync(entity);
//        public void Update(User entity)
//        {
//            _context.Users.Attach(entity);
//            _context.Users.Entry(entity).State = EntityState.Modified;
//        }


//        //public void Delete(User entity)
//        {
//            _context.Users.Remove(entity);
//        }

//        public async Task Save()
//        {
//            await _context.SaveChangesAsync();
//        }

//        public IEnumerable<User> Search(Func<User, bool> filter)
//            => _context.Users.Where(d => d.Name == "").ToList();

        
//    }
//}
