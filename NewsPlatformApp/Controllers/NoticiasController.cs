using Microsoft.AspNetCore.Mvc;
using NewsPlatformApp.Services;
using NewsPlatformApp.Models;
using NewsPlatformApp.Data;
using Microsoft.EntityFrameworkCore;

namespace NewsPlatformApp.Controllers
{
    public class NoticiasController : Controller
    {
        private readonly JsonPlaceholderService _jsonService;
        private readonly ApplicationDbContext _db;

        public NoticiasController(JsonPlaceholderService jsonService, ApplicationDbContext db)
        {
            _jsonService = jsonService;
            _db = db;
        }

        // GET: /Noticias
        public async Task<IActionResult> Index()
        {
            var posts = await _jsonService.GetPostsAsync();
            return View(posts);
        }

        // GET: /Noticias/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            // 1. Traer post, autor y comentarios de la API externa
            var post = await _jsonService.GetPostAsync(id);
            if (post == null)
                return NotFound();

            var autor = await _jsonService.GetUserAsync(post.UserId);
            var comentarios = await _jsonService.GetCommentsAsync(post.Id);

            // 2. Traer feedback local, si existe
            var feedback = await _db.Feedbacks.FirstOrDefaultAsync(f => f.PostId == id);

            // 3. Preparar viewmodel enriquecido
            var vm = new DetallePostViewModel
            {
                Post = post,
                Autor = autor,
                Comentarios = comentarios,
                Feedback = feedback
            };

            return View(vm);
        }
    }
}
