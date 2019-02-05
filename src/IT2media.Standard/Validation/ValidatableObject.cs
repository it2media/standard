using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IT2media.Standard.Validation
{
    public class ValidatableObject<TType> : INotifyPropertyChanged
    {
        private TType _value;
        private bool _isValid;

        public List<IValidationRule<TType>> Validations { get; } = new List<IValidationRule<TType>>();

        public ValidatableObject(params IValidationRule<TType>[] validations)
        {
            _isValid = true;
            foreach (IValidationRule<TType> validation in validations)
            {
                Validations.Add(validation);
            }
        }

        public TType Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        public string Messages
        {
            get
            {
                string messages = string.Empty;

                foreach (IValidationRule<TType> validationRule in Validations)
                {
                    if (!validationRule.Validate(_value))
                    {
                        messages += string.Join(Environment.NewLine, validationRule.Message);
                    }
                }

                return messages;
            }
        }

        public void Validate()
        {
            IsValid = Validations.TrueForAll(v => v.Validate(Value));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}