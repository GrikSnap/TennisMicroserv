using Microsoft.AspNetCore.Mvc;
using TennisMicroserv.Models;

namespace TennisMicroserv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly ILogger<PlayerController> _logger;
        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get players List
        /// Renvoi la liste des joueurs et les informations les concernants
        /// du meilleur au moin bon
        /// </summary>
        /// <returns>List<Player></returns>
        [Produces(typeof(List<Player>))]
        [HttpGet("players")]
        public List<Player> GetplayersList()
        {
            List<Player> result = null;
            var data = getData();
            if (data != null)
            {
                result = data.players.OrderBy(v => v.data.rank).ToList();
            }
            else
            {
                _logger.LogError($"Une erreur c'est produite lors de la récupération des informations");
            }
            return result;
            
        }

        /// <summary>
        /// GET PlayerById
        /// Retourne les informations d'un joueur via son ID
        /// </summary>
        /// <param name="idPlayer"></param>
        /// <returns>Player</returns>
        [Produces(typeof(Player))]
        [HttpGet("{idPlayer}")]
        public Player GetPlayersById(int idPlayer)
        {
            Player result = null;
            var data = getData();
            if (data != null)
            {
               result = data.players.FirstOrDefault(v => v.id.Equals(idPlayer));
            }  
            else
            {
                _logger.LogError($"l'ID saisie n'existe pas");
            }
            return result;
        }

        public class StatisticResult
        {
            public string? TopCountry { get; set; }
            public double MidIMC { get; set; }
            public double MedianHeight { get; set; }

        }
        /// <summary>
        /// Get Statistic
        /// Donne les statistiques suivantes :
        /// - Le pay au ratio de partie gagnées le plus grand 
        /// - L' IMC Moyen de tous les joueurs 
        /// - La mediane de la taille des joueurs 
        /// </summary>
        /// <returns>StatisticResult</returns>
        [Produces(typeof(StatisticResult))]
        [HttpGet("statistique")]
        public StatisticResult GetStatistic()
        {
            var data = getData();
            if (data != null)
            {
                Dictionary<string, double> countryRatio = new Dictionary<string, double>();
                double cumulIMC = default(float);

                if (data != null)
                {
                    List<double> ListHeights = new List<double>();

                    data.players.ForEach(p =>
                    {

                        //Le pays au ratio de partie gagnées le plus grand
                        if (!countryRatio.Keys.Contains(p.country.code))
                            countryRatio.Add(p.country.code, p.data.last.Where(d => d.Equals(1)).Count());
                        else
                            countryRatio[p.country.code] = (countryRatio[p.country.code] + p.data.last.Where(d => d.Equals(1)).Count()) / 2; //Non n'exact

                        ListHeights.Add(p.data.height);


                        //IMC Calule
                        cumulIMC = cumulIMC + p.data.height / (p.data.weight / 1000);
                    });

                    #region Calcul Median

                    var heights = ListHeights.ToArray();
                    double median;
                    if (heights.Length % 2 == 0)
                    {
                        median = (heights[heights.Length / 2 - 1] + heights[heights.Length / 2]) / 2;
                    }
                    else
                    {
                        median = heights[(heights.Length - 1) / 2];
                    }

                    #endregion

                    return new StatisticResult()
                    {
                        TopCountry = countryRatio.MaxBy(v => v.Value).Key,
                        MidIMC = cumulIMC / data.players.Count(),
                        MedianHeight = median,
                    };
                }
            }
            return new StatisticResult();
        }


        private Root getData()
        {
            Root result = null;
            try
            {
                result = (new PlayerContext()).root;
            }
            catch
            {
                _logger.LogError($"Une erreur c'est produite lors de la récupération des informations");
            }
            return result;
        }
    }
}