namespace Domain.Filters;

public class ValidFilter
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public ValidFilter()
    {

    }
    public ValidFilter(int pageSize, int pageNumber)
    {
        PageSize = pageSize < 1 ? 10 : pageSize;
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
    }
}

// tak stoy ti menya opyat ne pravilno ponyal blyat ya tebe po chasu pishu i obyasnyayu a ti pishesh chtoto tupoe blya 
// slushay kogda polzovatel delayet registratsiyu email soxranyayetsya a potom kogda on sdelal llogin i najal na reset (eta funcsiya [authorize]) email polzovatelya zapolnyayetsya avtomaticheski a ne tak kak ti pishesh chto nujno zapolnyat email avtomaticheski