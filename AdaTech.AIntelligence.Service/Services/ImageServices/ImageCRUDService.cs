using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using AdaTech.AIntelligence.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.ImageServices
{
    public class ImageCRUDService
    {
        private readonly IAIntelligenceRepository<Image> _repository;

        public ImageCRUDService(IAIntelligenceRepository<Image> repository)
        {
            _repository = repository;
        }

        //public async Task<bool> CreateImage(DTOImageRegister imageRequest)
        //{
        //    try
        //    {
        //        if (imageRequest.ByteImage == null && imageRequest.URLImage == null)
        //            throw new NotReadableImageException("Nenhum dado foi encontrado para a leitura da imagem.");

        //        var respostaObjeto = new Image()
        //        {
        //            SourceType,
        //            ProcessingStatus,
        //            ? Expense,
        //            ? ExpenseId,
        //        };



        //        var respostaObjeto = new Expense()
        //        {
        //            Category = (Category)int.Parse(valores[0]),
        //            TotalValue = double.Parse(valores[1].Replace(".", ",")),
        //            Description = valores[2],
        //            Status = ExpenseStatus.SUBMETIDO,
        //            IsActive = true
        //        };

        //        var success = await _repository.Create(respostaObjeto);

        //        return success;

        //    }
        //    catch
        //    {
        //        throw new Exception($"{response} \nVerifique possíveis problemas com a resolução da imagem enviada!");
        //    }

        //}
    }
}
