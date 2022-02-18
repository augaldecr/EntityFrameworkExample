using AutoMapper;
using EntityFrameworkExample.Entitites;
using System.Collections.ObjectModel;

namespace EntityFrameworkExample.Services
{
    public interface IUpdaterObservableCollection
    {
        void Update<ENT, DTO>(ObservableCollection<ENT> Entitites, IEnumerable<DTO> dtos)
            where ENT : IId
            where DTO : IId;
    }

    public class UpdaterObservableCollection: IUpdaterObservableCollection
    {
        private readonly IMapper _mapper;

        public UpdaterObservableCollection(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Update<ENT, DTO>(ObservableCollection<ENT> Entitites, IEnumerable<DTO> dtos)
            where ENT : IId
            where DTO : IId
        {
            var dictionaryEntitites = Entitites.ToDictionary(x => x.Id);
            var dictionaryDTOs = dtos.ToDictionary(x => x.Id);

            var idsEntitites = dictionaryEntitites.Select(x => x.Key);
            var idsDTOs = dictionaryDTOs.Select(x => x.Key);

            var create = idsDTOs.Except(idsEntitites);
            var delete = idsEntitites.Except(idsDTOs);
            var update = idsEntitites.Intersect(idsDTOs);

            foreach (var id in create)
            {
                var entity = _mapper.Map<ENT>(dictionaryDTOs[id]);
                Entitites.Add(entity);
            }

            foreach (var id in delete)
            {
                var entity = dictionaryEntitites[id];
                Entitites.Remove(entity);
            }

            foreach (var id in update)
            {
                var dto = dictionaryDTOs[id];
                var entity = dictionaryEntitites[id];
                entity = _mapper.Map(dto, entity);
            }
        }
    }
}
