using API.DTOs;
using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapping
{
    public class QuizProfile:Profile
    {
        public QuizProfile()
        {
            CreateMap<Antwoord, AntwoordDTO>();
            CreateMap<Quiz, QuizDTO>()
                .ForMember(q => q.Moeilijkheidsgraad, opt => opt.MapFrom(dest => dest.Moeilijkheidsgraad.Titel));
            CreateMap<Vraag, VraagDTO>();
            CreateMap<Moeilijkheidsgraad, MoeilijkheidsgraadDTO>();
            CreateMap<QuizGebruiker, QuizGebruikerDTO>();
                        
            CreateMap<SaveAntwoordDTO, Antwoord>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AntwoordDTO, Antwoord>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<SaveQuizDTO, Quiz>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());            
            CreateMap<SaveVraagDTO, Vraag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<VraagDTO, Vraag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MoeilijkheidsgraadDTO, Moeilijkheidsgraad>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<QuizGebruikerDTO, QuizGebruiker>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
