using Domain.Aggregates;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Constants;
using Application.Dtos;
using Application.Exceptions;
using Infrastructure.Repositories;

namespace Application.Services
{
    public abstract class ServiceBase<TRepository, TDocument, TDto> : IService<TDto>
        where TDocument : IDocument
        where TDto : DtoBase
        where TRepository : RepositoryBase<TDocument>
    {
        protected readonly IUnitOfWork Uow;
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        protected ServiceBase(IUnitOfWork uow, ILogger logger, IMapper mapper)
        {
            Uow = uow;
            Repository = Uow.GetRepository<TRepository>();
            Logger = logger;
            Mapper = mapper;
        }

        public virtual async Task<TDto?> GetByIdAsync(string id)
        {
            var document = await Repository.GetByIdAsync(id);

            if (document == null)
            {
                return null;
            }

            return Mapper.Map<TDocument, TDto>(document);
        }

        public virtual async Task<ListDtoResponse<TDto>> ListAsync(ListDtoRequest request)
        {
            var documentRequest = Mapper.Map<ListDocumentRequest>(request);

            var resp = await Repository.ListAsync(documentRequest);

            return Mapper.Map<ListDocumentResponse<TDocument>, ListDtoResponse<TDto>>(resp);
        }

        public virtual async Task AddAsync(TDto dto)
        {
            var document = Mapper.Map<TDto, TDocument>(dto);
            
            await Repository.InsertOneAsync(document);
            
            dto.Id = document.Id;
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            if (dto.Id == null)
            {
                throw new ValidationException(ErrorMessages.RecordNotFound);
            }

            var document = await GetByIdAsync(dto.Id);

            if (document == null)
            {
                throw new ValidationException(ErrorMessages.RecordNotFound);
            }

            var updatedDocument = Mapper.Map<TDto, TDocument>(dto);

            await Repository.ReplaceOneAsync(updatedDocument);
        }

        public virtual Task DeleteByIdAsync(string id)
        {
            return Repository.DeleteByIdAsync(id);
        }
    }
}