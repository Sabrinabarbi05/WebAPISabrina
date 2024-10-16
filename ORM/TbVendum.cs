using System;
using System.Collections.Generic;

namespace WebApiBia.ORM;

public partial class TbVendum
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public byte[]? NotaFiscal { get; set; }

    public int FkProduto { get; set; }

    public int FkCliente { get; set; }

    public virtual TbCliente FkClienteNavigation { get; set; } = null!;

    public virtual TbProduto FkProdutoNavigation { get; set; } = null!;
}
