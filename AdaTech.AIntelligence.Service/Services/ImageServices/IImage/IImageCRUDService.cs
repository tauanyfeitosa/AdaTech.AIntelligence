using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.ImageServices.IImage
{
    public interface IImageCRUDService
    {
        Task<bool> CreateImage(Image image);
        Task<bool> UpdateImage(Image image);
        Task<Image> GetOne(int idImage);
        Task<string> DeleteAsync(int id, bool isHardDelete);
    }
}
