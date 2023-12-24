import { readFile } from 'node:fs';

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    let info = input.toString("utf8").split('\n');
    // info = info.slice(0, 4); // Just some to test;

    function parseGame(gameString){
        let index = gameString.split(':')[0].slice('5');
        let content = gameString.slice(8);

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

    function main() {
        let parsedInfo = info.map(parseGame);
        let trueGamers = parsedInfo.map(extractTrueGamers).filter(x => x.possible);

        let sum = 0;
        trueGamers.forEach(game => {
            if (game.game){
                sum += Number(game.game)
            }
        });
        console.log(sum)
    };

    main()
});
