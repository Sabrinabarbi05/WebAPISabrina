using System;
using System.Collections.Generic;

namespace WebApiBia.ORM;

public partial class TbProduto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public int Quantidade { get; set; }

    public byte[]? NotaFiscal { get; set; }

    public virtual ICollection<TbVendum> TbVenda { get; set; } = new List<TbVendum>();
}
