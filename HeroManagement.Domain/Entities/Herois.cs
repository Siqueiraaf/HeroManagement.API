namespace HeroManagement.Domain;

public class Heroi
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string NomeHeroi { get; private set; } = null!;
    public DateTime DataNascimento { get; private set; }
    public float Altura { get; private set; }
    public float Peso { get; private set; }

    public ICollection<HeroiSuperpoder> HeroisSuperpoderes { get; private set; }
        = new List<HeroiSuperpoder>();

    private readonly List<int> _superpoderesIds = new List<int>();

    protected Heroi() { }

    public Heroi(string nome, string nomeHeroi, DateTime dataNascimento, float altura, float peso)
    {
        Nome = nome;
        NomeHeroi = nomeHeroi;
        DataNascimento = dataNascimento;
        Altura = altura;
        Peso = peso;
    }

    public void Atualizar(string nome, string nomeHeroi, DateTime dataNascimento, float altura, float peso, IEnumerable<int> superpoderesIds)
    {
        Nome = nome;
        NomeHeroi = nomeHeroi;
        DataNascimento = dataNascimento;
        Altura = altura;
        Peso = peso;

        AtualizarSuperpoderes(superpoderesIds);
    }

    private void AtualizarSuperpoderes(IEnumerable<int> novosSuperpoderes)
    {
        _superpoderesIds.Clear();

        foreach (var id in novosSuperpoderes.Distinct())
        {
            _superpoderesIds.Add(id);
        }
    }

    public void AdicionarSuperpoder(int superpoderId)
    {
        HeroisSuperpoderes.Add(new HeroiSuperpoder(Id, superpoderId));
    }
}
