import React, { useContext } from "react";
import { Link } from "react-router-dom";

import { BsPlus, BsEyeFill } from "react-icons/bs";

import { CartContext } from "../Contexts/CartContext";
import { ProductItem } from "../models/ProductModels";
import { CartItem } from "../models/CartModels";

const Product = (product: ProductItem) => {
  const { addToCart } = useContext(CartContext);
  const cartItem: CartItem = {
    productId: product.productId,
    title: product.name,
    thumbnail: product.thumbnail,
    quantity: product.quantity,
    unityPrice: product.price,
    totalAmount: product.price * product.quantity,
  };
  // destructure product

  return (
    <div>
      <div className="border border-[#e4e4e4] h-[300px] mb-4 relative overflow-hidden group transition">
        <div className="w-full h-full flex justify-center items-center">
          {/* image */}
          <div className="w-[200px] mx-auto flex justify-center items-center">
            <img
              className="max-h-[160px] group-hover:scale-110 transition duration-300"
              src={product.thumbnail}
              alt=""
            />
          </div>
        </div>
        {/* buttons */}
        <div className="absolute top-6 -right-11 group-hover:right-5 p-2 flex flex-col justify-center items-center gap-y-2 opacity-0 group-hover:opacity-100 transition-all duration-300">
          <button onClick={() => addToCart(cartItem)}>
            <div className="flex justify-center items-center text-white w-12 h-12 bg-teal-500">
              <BsPlus className="text-3xl" />
            </div>
          </button>
          <Link
            to={`/product/${product.productId}`}
            className="w-12 h-12 bg-white flex justify-center items-center text-primary drop-shadow-xl"
          >
            <BsEyeFill />
          </Link>
        </div>
      </div>
      {/* category, title & price */}
      <div>
        <div className="tex-sm capitalize text-gray-500 mb-1">
          {product.quantity} item
        </div>
        <Link to={`/product/${product.productId}`}>
          <h2 className="font-semibold mb-1">{product.name}</h2>
        </Link>

        <h2 className="font-semibbold">$ {product.price}</h2>
      </div>
    </div>
  );
};

export default Product;
