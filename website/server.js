var faker = require('faker'); 


function data(setup) {

	var cols = [
			{key: "id", pk: true},
			{key: "zipCode"},
			{key: "city"},
			{key: "cityPrefix"},
			{key: "citySuffix"},
			{key: "streetName"},
			{key: "streetAddress"},
			{key: "streetSuffix"},
			{key: "streetPrefix"},
			{key: "secondaryAddress"},
			{key: "county"},
			{key: "country"},
			{key: "countryCode"},
			{key: "state"},
			{key: "stateAbbr"},
			{key: "latitude"},
			{key: "longitude"} 
	];

	var rows = [];
	var rowCount = 1000;
	for (var i = 0; i < rowCount; i++) { 
		rows.push(
			{id : faker.random.number(),
			zipCode : faker.address.zipCode(),
			city : faker.address.city(),
			cityPrefix : faker.address.cityPrefix(),
			citySuffix : faker.address.citySuffix(),
			streetName : faker.address.streetName(),
			streetAddress : faker.address.streetAddress(),
			streetSuffix : faker.address.streetSuffix(),
			streetPrefix : faker.address.streetPrefix(),
			secondaryAddress : faker.address.secondaryAddress(),
			county : faker.address.county(),
			country : faker.address.country(),
			countryCode : faker.address.countryCode(),
			state : faker.address.state(),
			stateAbbr : faker.address.stateAbbr(),
			latitude : faker.address.latitude(),
			longitude : faker.address.longitude()}
		);
	};

	return {
		cols : cols,
		rows : rows,
		page : setup.page,
		pageSize : setup.pageSize,
	}
};


var express = require('express');
var app = express();
app.use(express.static('.'));

 
app.get('/api/storage/:name/:sortdirection', function (req, res) {
	var config = {};
  	res.send(data(config));
});

app.post('/api/storage/:name/:sortdirection', function (req, res) {
	var config = {};
  	res.send(data(config));
});

app.listen(3000, function () {
  console.log('Example app listening on port 3000!');
}); 