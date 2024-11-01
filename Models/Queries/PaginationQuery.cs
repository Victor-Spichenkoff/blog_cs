namespace blog_c_.Models.Queries;

public class PaginationQuery
{
    // props normal
    public int Page { get; set; }
    
    // valor real, controlado
    private int _pageSize = 10; // default
    
    // acessa / recebe daqui
    public int PageSize
    {
        // define como pegar o valor
        get => _pageSize; // arrow
        
        set => _pageSize = (value > 20 || value < - 20) ? 20 : Math.Abs(value); // value == recebido
        //<Pagination>PageSize = 45435
    }
}