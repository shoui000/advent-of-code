import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    let info = parseData(input.toString('utf8').split('\n').slice(0, -1));

    const cardCollection = findWinners(info);
    const realCards = getPrizes(cardCollection);

    let sumFirst = 0;
    cardCollection.forEach(card => {
        if (card.matches != 0){
            sumFirst += 2 ** (card.matches-1)
        }
    });

    console.log('First Puzzle:', sumFirst);
    console.log('Second Puzzle:', realCards.length);

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
            let matches = 0;
            card.winningNumbers.forEach(wNumber => {
                if (card.numbers.includes(wNumber)){
                    matches += 1
                };
            });
            card.matches = matches;
            return card;
        });
    };

    function getPrizes(cardCollection){
        let pile = [];
        recursiveFindCards(cardCollection, pile);
        function recursiveFindCards(cardPile, pile){
            cardPile.forEach(card => {
                pile.push(card);
                let childCards = cardCollection.slice(card.id, card.id+card.matches);
                if (childCards != 0){
                   recursiveFindCards(childCards, pile);
                }
            });
        };
        return pile;
    };
});
