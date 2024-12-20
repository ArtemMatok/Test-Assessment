using ETL.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    public class TaxiTripValidator : AbstractValidator<TaxiTrip>
    {
        public TaxiTripValidator()
        {
            RuleFor(trip => trip.PickupDateTime)
                .NotEmpty().WithMessage("PickupDateTime is required.")
                .Must(BeAValidDate).WithMessage("PickupDateTime must be a valid date.");

            RuleFor(trip => trip.DropoffDateTime)
                .NotEmpty().WithMessage("DropoffDateTime is required.")
                .Must(BeAValidDate).WithMessage("DropoffDateTime must be a valid date.")
                .Must((trip, dropoff) => IsDropoffLaterThanPickup(trip.PickupDateTime, dropoff))
                .WithMessage("DropoffDateTime must be later than PickupDateTime.");

            RuleFor(trip => trip.PassengerCount)
                .NotNull().WithMessage("PassengerCount is required.")
                .GreaterThanOrEqualTo(1).WithMessage("PassengerCount must be at least 1.")
                .Must(BeAnInteger).WithMessage("PassengerCount must be an integer.");

            RuleFor(trip => trip.TripDistance)
                .GreaterThan(0).WithMessage("TripDistance must be greater than 0.")
                .Must(BeADecimal).WithMessage("TripDistance must be a valid decimal value.");

            RuleFor(trip => trip.StoreAndFwdFlag)
                .NotEmpty().WithMessage("StoreAndFwdFlag is required.")
                .Must(flag => flag == "Yes" || flag == "No")
                .WithMessage("StoreAndFwdFlag must be either 'Yes' or 'No'.");

            RuleFor(trip => trip.FareAmount)
                .GreaterThanOrEqualTo(0).WithMessage("FareAmount must be non-negative.")
                .Must(BeADecimal).WithMessage("FareAmount must be a valid decimal value.");

            RuleFor(trip => trip.TipAmount)
                .GreaterThanOrEqualTo(0).WithMessage("TipAmount must be non-negative.")
                .Must(BeADecimal).WithMessage("TipAmount must be a valid decimal value.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default;
        }

        private bool IsDropoffLaterThanPickup(DateTime pickup, DateTime dropoff)
        {
            return dropoff > pickup;
        }

        private bool BeAnInteger(int? value)
        {
            return value.HasValue && value.Value == (int)value;
        }

        private bool BeADecimal(decimal value)
        {
            return decimal.TryParse(value.ToString(), out _);
        }
    }
}
