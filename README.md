# Standard

## Validation
The ValidationObject is used to validate user input. 

### Sample 

```c#
private readonly ValidatableObject<string> _mail;
private readonly ValidatableObject<string> _iban;

private const string MailMessage = "Wrong Mail";
private const string MandatoryMessage = "Field is empty";
private const string IbanMessage ="Wrong Iban";

public ValidationSample()
{
    _mail = new ValidatableObject<string>(new EmailRule(MailMessage),
        new MandatoryRule<string>(MandatoryMessage))
            { Value = "muster@mann.de"};
    _iban = new ValidatableObject<string>(new IbanRule(IbanMessage))
            { Value = "DE00 0800 0000 0000 0000 0000"};
}

public void Validate()
{
    _mail.Validate();
    _iban.Validate();
}    
```

### Sample Xamarin
#### ViewModel
```c#
public class ViewModel : ViewModelBase
{
    private ValidatableObject<string> _mail;
    public ICommand ValidateCommand => new Command(Validate);

    public ViewModel() : base()
    {
        Mail = new ValidatableObject<string>(new EmailRule("Error"));
    }

    private void Validate()
    {
        _mail.Validate();
    }

    public ValidatableObject<string> Mail
    {
        get => _mail;
        set
        {
            _mail = value;
            RaisePropertyChanged();
        } 
    }
}
```

#### xaml
```xaml
<Entry Text="{Binding Mail.Value}">
    <Entry.Behaviors>
        <behaviors:EventToCommandBehavior EventName="TextChanged" Command="{Binding ValidateCommand}" />
    </Entry.Behaviors>
    <Entry.Triggers>
        <DataTrigger TargetType="Entry" Binding="{Binding Mail.IsValid}" Value="True">
            <Setter Property="behavior:LineColorBehavior.LineColor" Value="{StaticResource ErrorColor}" />
        </DataTrigger>
    </Entry.Triggers>
</Entry>
<Button Text="Validierung" Command="{Binding ValidateCommand}"/>
```