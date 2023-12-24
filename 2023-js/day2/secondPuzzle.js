import { readFile } from 'node:fs'

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

    function extractPowers (game){
        let power = game.colors.red * game.colors.blue * game.colors.green;
        return {game: game.game, power: power, colors: {...game.colors}};
    };

    function main() {
        let parsedInfo = info.map(parseGame);
        let extractedPowers = parsedInfo.map(extractPowers).filter(x => x.game != '');


        let sum = 0;
        extractedPowers.forEach(game => {
            sum += game.power;
        });
        console.log(sum)

    };

    main()
});
