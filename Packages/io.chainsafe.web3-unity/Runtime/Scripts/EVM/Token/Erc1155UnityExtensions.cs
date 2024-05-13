﻿using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;
using ChainSafe.Gaming.Evm.Contracts.BuiltIn;
using ChainSafe.Gaming.Ipfs;
using ChainSafe.Gaming.Web3;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.EVM.Token
{
    public static class Erc1155UnityExtensions
    {
        [Pure]
        public static async Task<Texture2D> ImportTexture(Erc1155Contract contract, string tokenId)
        {
            // fetch uri from chain
            var uri = await contract.GetUri(tokenId);

            // fetch metadata from uri
            var metaRequest = UnityWebRequest.Get(uri);
            
            if (metaRequest.result != UnityWebRequest.Result.Success)
            {
                throw new Web3Exception($"Metadata request failure: {metaRequest.error}");
            }

            // prepare texture uri
            var metadata = JsonUtility.FromJson<Erc1155Metadata>(Encoding.UTF8.GetString(metaRequest.downloadHandler.data));
            var textureUri = IpfsHelper.RollupIpfsUri(metadata.image);
            
            // fetch texture
            var textureRequest = UnityWebRequestTexture.GetTexture(textureUri);
            await textureRequest.SendWebRequest();
            
            if (textureRequest.result != UnityWebRequest.Result.Success)
            {
                throw new Web3Exception($"Texture request failure: {metaRequest.error}");
            }
            
            var texture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;

            return texture;
        }
    }
}