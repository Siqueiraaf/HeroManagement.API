namespace HeroManagement.Domain;

public class Heroi
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string NomeHeroi { get; private set; } = null!;
    public DateTime DataNascimento { get; private set; }
    public double Altura { get; private set; }
    public double Peso { get; private set; }

    public ICollection<HeroiSuperpoder> HeroisSuperpoderes { get; private set; }
        = new List<HeroiSuperpoder>();

    protected Heroi() { }

    public Heroi(string nome, string nomeHeroi, DateTime dataNascimento, double altura, double peso)
    {
        Nome = nome;
        NomeHeroi = nomeHeroi;
        DataNascimento = dataNascimento;
        Altura = altura;
        Peso = peso;
    }

    public void AdicionarSuperpoder(int superpoderId)
    {
        HeroisSuperpoderes.Add(new HeroiSuperpoder(Id, superpoderId));
    }
}
