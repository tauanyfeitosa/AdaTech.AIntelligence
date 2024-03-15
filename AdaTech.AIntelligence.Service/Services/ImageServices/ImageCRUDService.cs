using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.Service.Services.ImageServices.IImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.ImageServices
{
    public class ImageCRUDService : IImageCRUDService
    {
        private readonly IAIntelligenceRepository<Image> _repository;
        private readonly GenericDeleteService<Image> _deleteService;

        public ImageCRUDService(IAIntelligenceRepository<Image> repository, GenericDeleteService<Image> deleteService)
        {
            _repository = repository;
            _deleteService = deleteService;
        }

        public async Task<bool> CreateImage(Image image)
        {
            var success = await _repository.Create(image);

            return success;
        }

        public async Task<bool> UpdateImage(Image image)
        {
            return await _repository.Update(image);
        }

        public async Task<Image> GetOne(int idImage)
        {
            var image = await _repository.GetOne(idImage);

            if (image != null)
                return image;

            throw new NotFoundException("Não foi localizada uma imagem com o ID fornecido. Tente novamente.");
        }
        
        public async Task<string> DeleteAsync(int id, bool isHardDelete)
        {
            return await _deleteService.DeleteAsync(_repository, id, isHardDelete);
        }
    }
}
