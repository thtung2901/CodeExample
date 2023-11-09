using STSLeave.Core.Helpers.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public class MonthlyPayrollCheckedPeriodModel
    {
        private readonly DateTime _currentPayrollMonth;
        private readonly DateTime? _probationContractStartDate;
        private readonly DateTime? _officialContractStartDate;
        private readonly DateTime? _resignDate;

        public MonthlyPayrollCheckedPeriodModel(DateTime currentPayrollMonth, DateTime? probationContractStartDate, DateTime? officialContractStartDate, DateTime? resignDate)
        {
            _currentPayrollMonth = currentPayrollMonth;
            _probationContractStartDate = probationContractStartDate;
            _officialContractStartDate = officialContractStartDate;
            _resignDate = resignDate;
        }

        public bool HasProbationContractPeriod => ProbationContractPeriod != null;

        public DateTimePeriod ProbationContractPeriod
        {
            get
            {
                if (_probationContractStartDate.GetValueOrDefault(DateTime.MaxValue).IsAfterMonth(_currentPayrollMonth) ||
                    _officialContractStartDate.HasValue && _officialContractStartDate.Value.IsBeforeMonth(_currentPayrollMonth))
                    return null;

                var from = _probationContractStartDate.GetValueOrDefault().IsSameMonth(_currentPayrollMonth)
                    ? _probationContractStartDate.GetValueOrDefault()
                    : _currentPayrollMonth.FirstDateOfMonth();

                var to = _officialContractStartDate.GetValueOrDefault().IsSameMonth(_currentPayrollMonth)
                    ? _officialContractStartDate.GetValueOrDefault().ADayBefore()
                    : _resignDate.GetValueOrDefault().IsSameMonth(_currentPayrollMonth)
                        ? _resignDate.GetValueOrDefault().ADayBefore()
                        : _currentPayrollMonth.LastDayOfMonth();

                return new DateTimePeriod(from, to);
            }
        }

        public bool HasOfficialContractPeriod => OfficialContractPeriod != null;

        public DateTimePeriod OfficialContractPeriod
        {
            get
            {
                if (_officialContractStartDate.GetValueOrDefault(DateTime.MaxValue).IsAfterMonth(_currentPayrollMonth)) return null;

                var from = _officialContractStartDate.GetValueOrDefault().IsSameMonth(_currentPayrollMonth) ?
                    _officialContractStartDate.GetValueOrDefault() :
                    _currentPayrollMonth.FirstDateOfMonth();

                var to = _resignDate.GetValueOrDefault().IsSameMonth(_currentPayrollMonth)
                        ? _resignDate.GetValueOrDefault().ADayBefore()
                        : _currentPayrollMonth.LastDayOfMonth();

                return new DateTimePeriod(from, to);
            }
        }

        public bool HasResignationDate => _resignDate.HasValue && _resignDate.Value.ADayBefore() <= _currentPayrollMonth.LastDayOfMonth().EndOfDay();
    }

    public class DateTimePeriod
    {
        public DateTimePeriod(DateTime from, DateTime to)
        {
            From = from.Date;
            To = to.EndOfDay();
        }

        public DateTime From { get; }
        public DateTime To { get; }

        public bool IsInPeriod(DateTime inputDate)
        {
            return inputDate >= From && inputDate <= To;
        }
    }
}
