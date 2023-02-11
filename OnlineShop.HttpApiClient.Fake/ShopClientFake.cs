using System.Collections.Concurrent;
using OnlineShop.Domain.Entities;
using OnlineShop.HttpModels.Request;
using OnlineShop.HttpModels.Response;

namespace OnlineShop.HttpApiClient.Fake;

public class ShopClientFake: IShopClient
{
    private readonly ConcurrentDictionary<Guid, Product?> _products;

    public ShopClientFake()
    {
        Product p1 = new Product(
            Guid.NewGuid(),
            "Введение в психиатрию и психоанализ для непосвященных",
            "Эрик Леннард Берн (1910—1970) — одна из ключевых фигур в современной психологии," +
            " создатель оригинального направления в психиатрии — «трансакционный анализ»," +
            " который рассматривает игру как основу человеческих взаимоотношений." +
            " Автор мирового бестселлера «Игры, в которые играют люди." +
            " С этой книги стоит начать знакомство с психиатрией и психологией." +
            " Во «Введении» Эрик Берн простым и предельно ясным языком рассказывает:" +
            " как устроена человеческая психика, что влияет на ее формирование," +
            " какие этапы развития проходит младенец и как они впоследствии отражаются на его жизни," +
            " что есть сны и неврозы, каковы скрытые причины алкоголизма," +
            " склонности к беспорядочным связям, затяжным депрессиям многое другое." +
            " Произведение Берна до сих пор по праву считается одним из самых полных и" +
            " увлекательных учебников по психиатрии и психоанализу." +
            " Это книга, к которой хочется возвращаться снова и снова, расширяя свою картину мира и" +
            " погружаясь в увлекательный мир человеческой психики.",
            11,
            "1.jpg"
        );
        Product p2 = new Product(
            Guid.NewGuid(),
            "Сценарии жизни людей",
            "В своей книге признанный мастер практической психологии Клод Штайнер," +
            " ближайший последователь Эрика Берна, описывает три губительных сценария жизни -" +
            " 'без любви', 'без разума' и 'без радости', - предлагая читателю механизм изменения своей 'судьбы'." +
            "Впервые увидевшая свет в 1974 году, эта теория выдержала проверку временем и" +
            " оказалась универсальной и интернациональной, что определило успех книги," +
            " ставшей мировым бестселлером.",
            9,
            "2.jpg"
        );
        Product p3 = new Product(
            Guid.NewGuid(),
            "Истинная правда, или Учебник для психолога по жизни",
            "Это книга о вкусной и здоровой жизни. Как живой и пристрастный собеседник," +
            " она поможет обычному человеку открыть в себе психолога - психолога-практика," +
            " психолога по жизни, а тем, кто уже психолог, - не потерять в себе Человека." +
            " Поскольку истинная правда всегда объемна, то книга вам поможет увидеть, кроме правды своей," +
            " правду другого человека, совсем разную правду мужчин и женщин и правду общечеловеческую -" +
            " правду детей, которыми, похоже, является каждый из нас. Книга всерьез отвечает на много" +
            " веселых КАК. Специально для молодежи - как знакомиться на улице, для всех - в какие игры" +
            " мы играем, когда просто живем, зачем мы ссоримся и как жить по-другому, о силе слабости," +
            " чем по правде определяется наше отношение к изменам и каков Кодекс порядочного человека -" +
            " в общем, как стать богатым и здоровым, а не бедным и больным. На страницах -" +
            " психологические практикумы и тесты, медитации и провокации, деловая информация и" +
            " нужные анекдоты, то есть все, чем может и должна быть наполнена реальная жизнь." +
            " Заодно узнаете о жизни психолога изнутри, зачем и какая психология нужна именно вам," +
            " что такое Lifespring, НЛП и другие психологические тренинги для нормального человека," +
            " познакомитесь с жизнью удивительного Клуба 'Синтон' и как стать в жизни - Мастером. " +
            "Вот так простенько и все под одной обложкой. Хорошая встреча!",
            10,
            "3.jpg"
        );
        _products = new()
        {
            [p1.Id] = p1, [p2.Id] = p2, [p3.Id] = p3
        };
    }
    
    public Task<IReadOnlyList<Product>> GetProducts(CancellationToken cancellationToken)
    {
        var collection = _products.Values.ToList().AsReadOnly();
        return Task.FromResult((IReadOnlyList<Product>)collection);
    }

    public Task AddProduct(Product product, CancellationToken cancellationToken = default)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        _products.TryAdd(product.Id, product);
        return Task.CompletedTask;
    }

    public Task<Product?> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_products[id]);
    }

    public Task UpdateProduct(Product newProduct, Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<LogInResponse> Register(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<LogInResponse> LogIn(LogInRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void SetAuthToken(string token)
    {
        throw new NotImplementedException();
    }
}