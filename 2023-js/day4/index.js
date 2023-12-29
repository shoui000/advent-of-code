import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    const info = parseData(input.toString('utf8').split('\n').slice(0, -1));
    const cardCollection = findWinners(info);

    let sumFirst = 0;
    cardCollection.forEach(card => {
        sumFirst += card.points
    });
    console.log(sumFirst);

    function parseData(data){
        return data.map(line => {
            let [index, content] = line.split(':');

            index = index.split(' ');
            index = Number(index[index.length-1]);
            
            let [winningNumbers, numbers] = content.split('|');

            [winningNumbers, numbers] = [winningNumbers, numbers].map(content => {
                let array = content.split(' ').map(number => {
                    if (number.match(/[\w\d]+/)){
                        return Number(number)
                    }
                }).filter(x => x != undefined);
                return array;
            });

            let card = {id: index, winningNumbers: winningNumbers, numbers: numbers};
            return card;
        });
    };

    function findWinners(cardCollection){
        return cardCollection.map(card => {
            let points = 0;
            card.winningNumbers.forEach(wNumber => {
                if (card.numbers.includes(wNumber)){
                    if (points == 0){
                        points = 1
                    } else {
                        points = points * 2
                    };
                };
            });
            card.points = points;
            return card;
        });
    };
});
