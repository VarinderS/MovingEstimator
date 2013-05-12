var App = Ember.Application.create();

App.Store = DS.Store.extend({
    revision: 12,
    adapter: DS.FixtureAdapter
});

App.Price = DS.Model.extend({
    
});