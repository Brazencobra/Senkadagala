using Microsoft.VisualBasic;
using Senkadagala.Models;
using System.Security.Principal;

namespace Senkadagala.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Senkadagala.Models.Information>? Information { get; set; }
        public IEnumerable<Setting>? Settings { get; set; }
    }
}
