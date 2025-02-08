using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.Shared.Mappings
{
    public class CardsProfile : Profile
    {
        public CardsProfile()
        {
            CreateMap<Card, CardReadDetailDTO>()
                .ForMember(dto => dto.ArtistFullName,
                            opt => opt.MapFrom(c => c.Artist.FullName))
                .ForMember(dto => dto.CardColors,
                            opt => opt.MapFrom(c => c.CardColors.Select(cc => cc.Color.Name)))
                .ForMember(dto => dto.CardTypes,
                            opt => opt.MapFrom(c => c.CardTypes.Select(ct => ct.Type.Name)))
                .ForMember(dto => dto.Rarity,
                            opt => opt.MapFrom(c => c.RarityCodeNavigation.Name))
                .ForMember(dto => dto.SetName,
                            opt => opt.MapFrom(c => c.SetCodeNavigation.Name));


            CreateMap<Card, CardReadBasicDTO>()
               .ForMember(dto => dto.CardColors,
                           opt => opt.MapFrom(c => c.CardColors.Select(cc => cc.Color.Name)))
               .ForMember(dto => dto.Rarity,
                           opt => opt.MapFrom(c => c.RarityCodeNavigation.Name));
        }
    }
}
