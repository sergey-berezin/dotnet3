using PlotterViewModel;
using System;
using Xunit;
using Moq;
using System.Security.RightsManagement;
using System.Linq;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using OxyPlot.Series;
using OxyPlot;

namespace PlotterViewModelTests
{
    public class MainViewModelTests
    {
        [Fact]
        public void ErrorScenario()
        {
            var er = new Mock<IUIServices>();
            var mvm = new MainViewModel(er.Object);

            mvm.Expression = "qwerty";
            mvm.PlotCommand.Execute(null);

            er.Verify(r => r.ReportError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void BasicScenario()
        {
            var er = new Mock<IUIServices>();

            var mvm = new MainViewModel(er.Object);
            mvm.Expression = "x*x";
            mvm.PlotCommand.Execute(null);

            er.Verify(r => r.ReportError(It.IsAny<string>()), Times.Never);

            mvm.TableData.Should().BeInAscendingOrder(new RowViewModelComparer());
            mvm.TableData.Should().OnlyContain(vm => Math.Abs(vm.X * vm.X - vm.F) < 1e-10);

            mvm.ChartData.Series.Should().HaveCount(1);
            mvm.ChartData.Series[0].Should().BeAssignableTo<LineSeries>();

            var lineSeries = (LineSeries)mvm.ChartData.Series[0];
            lineSeries.Points.Should().BeInAscendingOrder(new DataPointComparer());
            lineSeries.Points.Should().OnlyContain(p => Math.Abs(p.X * p.X - p.Y) < 1e-10);
        }

        private class RowViewModelComparer : IComparer<RowViewModel>
        {
            public int Compare(RowViewModel x, RowViewModel y)
            {
                return Math.Sign(x.X - y.X);
            }
        }

        private class DataPointComparer : IComparer<OxyPlot.DataPoint>
        {
            public int Compare(DataPoint x, DataPoint y)
            {
                return Math.Sign(x.X - y.X);
            }
        }
    }
}
