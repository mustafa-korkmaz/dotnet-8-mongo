using Application.Dtos;

namespace Application.Services
{
    public interface IService<TDto> where TDto : DtoBase
    {
        Task<ListDtoResponse<TDto>> ListAsync(ListDtoRequest request);

        /// <summary>
        /// returns dto object by given id
        /// </summary>
        /// <param name="id"></param>
        Task<TDto?> GetByIdAsync(string id);

        /// <summary>
        /// creates new entity from given dto
        /// </summary>
        /// <param name="dto"></param>
        Task AddAsync(TDto dto);

        /// <summary>
        /// updates given entity and returns affected row count.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>affected row count in db</returns>
        Task UpdateAsync(TDto dto);

        /// <summary>
        /// hard or soft deletes entity by given id
        /// </summary>
        /// <param name="id"></param>
        Task DeleteByIdAsync(string id);
    }
}