using Xunit;

using System.Linq;
using System;
using ListaDubluInlantuita;

namespace ListaSimpluInlantuita
{
    public class ListaDubluInlantuitaTests
    {
        [Fact]
        public void Jmecherie()
        {
            var listaJmechera = new ListaDubluInlantuita<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var sumOfSquareEvens = listaJmechera
                .Where(muieLista => muieLista % 2 == 0)
                .Select(muieLista => Math.Pow(muieLista, 2))
                .Aggregate((muieLista, otherMuieLista) => muieLista + otherMuieLista);

            Assert.Equal(220, sumOfSquareEvens);
        }
    }
}
