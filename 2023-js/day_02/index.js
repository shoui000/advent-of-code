import { readFile } from 'node:fs';

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    let info = input.toString("utf8").split('\n').slice(0, -1);
    // info = info.slice(0, 4); // Just some to test;

    function parseGame(gameString){
        let index = gameString.split(':')[0].slice('5');
        let content = gameString.split(':')[1];

        let sets = content.split(';');
        let parsedSets = sets.map(set => set = set.split(',').map(x => x.trim()));

        function createObjectSet(set){
            let object = {red: 0, green: 0, blue: 0};

            set.forEach(cube => {
                Object.keys(object).forEach(possibleColor => {
                    if (cube.search(possibleColor) > -1){
                        object[possibleColor] = Number(cube.split(' ')[0])
                    }

                });
            });

            return object;
        };

        let matrix = parsedSets.map(createObjectSet);

        function takesMaxColors(matrix){
            let maxColors = {red: 0, blue: 0, green: 0}

            matrix.forEach(set => {
                Object.keys(set).forEach(color => {
                    if (set[color] > maxColors[color]){
                        maxColors[color] = set[color]
                    }
                })
            });

            return maxColors;
        };

        let matrixMax = takesMaxColors(matrix);
        return {game: index, colors: matrixMax};
    };

    function extractTrueGamers (game){
        let maxpossible = {red: 12, green: 13, blue: 14};
        let isPossible = true;

        Object.keys(game.colors).forEach(color => {
            if (game.colors[color] > maxpossible[color]) isPossible = false;
        });

        return {game: game.game, possible: isPossible};
    };

    function extractPowers (game){
        let power = game.colors.red * game.colors.blue * game.colors.green;
        return {game: game.game, power: power, colors: {...game.colors}};
    };

    function main() {
        let parsedInfo = info.map(parseGame);
        let trueGamers = parsedInfo.map(extractTrueGamers).filter(x => x.possible);
        let extractedPowers = parsedInfo.map(extractPowers).filter(x => x.game != '');

        let sumFirst = 0;
        trueGamers.forEach(game => {
            if (game.game){
                sumFirst += Number(game.game)
            }
        });

        let sumSecond = 0;
        extractedPowers.forEach(game => {
            sumSecond += game.power;
        });

        console.log(`First Answer: ${sumFirst}`);
        console.log(`Second Answer: ${sumSecond}`);
    };

    main()
});
