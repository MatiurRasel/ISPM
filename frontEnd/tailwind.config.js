/** @type {import('tailwindcss').Config} */

const path = require('path');
const colors = require('tailwindcss/colors');
const defaultTheme = require('tailwindcss/defaultTheme');
const generatePalette = require(
  path.resolve(__dirname,
    ('src/'))
);


module.exports = {
  content: [],
  theme: {
    extend: {},
  },
  plugins: [],
}

