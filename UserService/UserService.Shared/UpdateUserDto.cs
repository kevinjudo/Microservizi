public class UpdateUserDto
{
    public int Id { get; set; } // L'ID dell'utente da aggiornare
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; } // In formato "yyyy-MM-dd"
}
