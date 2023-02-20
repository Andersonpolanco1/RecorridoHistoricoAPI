﻿using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Models.DTOs.Horario
{
    public class HorarioCreateDto
    {
        public int TandaId { get; set; }
        public DiaSemana Dia { get; set; }
        public string Hora { get; set; } = string.Empty;
    }
}


