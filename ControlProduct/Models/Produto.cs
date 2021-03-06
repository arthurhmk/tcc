﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using ControlProduct.Models.Enum;

namespace ControlProduct.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [DisplayName("Categoria do Produto")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage ="Insira o nome do produto.")]
        [DisplayName("Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [DisplayName("Preço")]
        public double Valor{ get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [DisplayName("Quantidades em estoque")]
        public int Quantidade { get; set; }

        [DisplayName("Disponibilidade")]
        public EstadoProduto Ativo { get; set; }

        public Categoria Categoria { get; set; }

        public List<PedidoProduto> PedidoProdutos { get; set; }
    }
}
